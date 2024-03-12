using HospitalManagementSystem.Models;
using HospitalManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class PatientController : Controller
{
    private HospitalManagementContext dbContext = new HospitalManagementContext();

    /// <summary>
    /// Retrieves and calculates the age distribution based on patients with the medical condition "Hypertension."
    /// </summary>
    /// <returns>An ActionResult representing the age distribution view.</returns>
    public ActionResult AgeDistribution()
    {
        // Get distinct patient IDs with the medical condition "Hypertension"
        var distinctPatientIds = dbContext.MedicalHistories
            .Where(mh => mh.MedicalCondition == "Hypertension")
            .Select(mh => mh.PatientId)
            .Distinct();

        // Join distinct patient IDs with patient information and group by gender
        var result = from patientId in distinctPatientIds
                     join patient in dbContext.Patients on patientId equals patient.PatientId
                     group patient by patient.Gender into genderGroup
                     select new AgeDistributionViewModel
                     {
                         Gender = genderGroup.Key,
                         AverageAge = genderGroup.Average(p => p.Age),
                         MedicalCondition = "Hypertension"
                     };

        return View(result.ToList());
    }

    /// <summary>
    /// Retrieves treatment types and associated patient information for patients with the medical condition "Hypertension."
    /// </summary>
    /// <returns>An ActionResult representing the view with treatment types and patient information.</returns>
    public ActionResult TreatmentTypesForPatients()
    {
        // Query medical histories to filter by the medical condition "Hypertension"
        var result = dbContext.MedicalHistories
            .Where(mh => mh.MedicalCondition == "Hypertension")
            .GroupBy(mh => mh.Treatment)
            .Select(group => new TreatmentViewModel
            {
                TreatmentType = group.Key,
                Patients = group.Select(mh => new PatientInfoViewModel
                {
                    FirstName = mh.Patient.FirstName,
                    Age = mh.Patient.Age
                }).ToList()
            })
            .ToList();

        return View(result);
    }

    /// <summary>
    /// Retrieves the count of all treatments for all patients, including patient information.
    /// </summary>
    /// <returns>An IActionResult representing the view with treatment counts and patient information.</returns>
    public IActionResult AllTreatmentsCountForAllPatients()
    {
        // Query medical histories and group by PatientId and Treatment, then count the occurrences
        var result = dbContext.MedicalHistories
            .GroupBy(mh => new { mh.PatientId, mh.Treatment })
            .Select(group => new PatientTreatmentCountViewModel
            {
                PatientId = group.Key.PatientId,
                TreatmentType = group.Key.Treatment,
                Count = group.Count()
            })
            .OrderBy(result => result.PatientId)
            .ToList();

        // Fetch patient information for each item in the result
        foreach (var item in result)
        {
            var patient = dbContext.Patients.FirstOrDefault(p => p.PatientId == item.PatientId);
            if (patient != null)
            {
                item.FirstName = patient.FirstName;
            }
        }

        return View(result);
    }

    /// <summary>
    /// Retrieves the count of occurrences for each medical condition for all patients, including patient information.
    /// </summary>
    /// <returns>An IActionResult representing the view with medical condition counts and patient information.</returns>
    public IActionResult AllMedicalConditionsCountForAllPatients()
    {
        // Query medical histories and group by PatientId and MedicalCondition, then count the occurrences
        var result = dbContext.MedicalHistories
            .GroupBy(mh => new { mh.PatientId, mh.MedicalCondition })
            .Select(group => new PatientMedicalConditionCountViewModel
            {
                PatientId = group.Key.PatientId,
                MedicalCondition = group.Key.MedicalCondition,
                Count = group.Count()
            })
            .OrderBy(result => result.PatientId)
            .ToList();

        // Fetch patient information for each item in the result
        foreach (var item in result)
        {
            var patient = dbContext.Patients.FirstOrDefault(p => p.PatientId == item.PatientId);
            if (patient != null)
            {
                item.FirstName = patient.FirstName;
            }
        }

        return View(result);
    }

    /// <summary>
    /// Retrieves information about patients who have had unsuccessful treatments, including details about the treatments and patient demographics.
    /// </summary>
    /// <returns>An IActionResult representing the view with information about patients with unsuccessful treatments.</returns>
    public IActionResult PatientUnSuccessfulTreatments()
    {
        // Query treatment records and group by PatientId, TreatmentType, Patient.FirstName, Patient.Age, and Patient.Address
        // Count the occurrences of unsuccessful treatments for each patient
        var result = dbContext.TreatmentRecords
            .GroupBy(tr => new { tr.PatientId, tr.TreatmentType, tr.Patient.FirstName, tr.Patient.Age, tr.Patient.Address })
            .Select(group => new PatientUnSuccessfulTreatmentsViewModel
            {
                PatientId = group.Key.PatientId,
                FirstName = group.Key.FirstName,
                Age = group.Key.Age,
                Address = group.Key.Address,
                TreatmentType = group.Key.TreatmentType,
                UnsuccessfulCount = group.Count(tr => tr.Outcome == "Failed")
            })
            .Where(p => p.UnsuccessfulCount > 0)
            .OrderBy(result => result.PatientId)
            .ToList();

        return View(result);
    }

    /// <summary>
    /// Retrieves the success rate for treatments related to the medical condition "Hypertension" for patients.
    /// </summary>
    /// <returns>An IActionResult representing the view with success rates for treatments.</returns>
    public IActionResult SuccessRateForHypertension()
    {
        // Join Patients, MedicalHistories, and TreatmentRecords tables to gather relevant data
        var treatmentSuccessRates = dbContext.Patients
            .Join(dbContext.MedicalHistories, patient => patient.PatientId, medicalHistory => medicalHistory.PatientId, (patient, medicalHistory) => new { patient, medicalHistory })
            .Join(dbContext.TreatmentRecords, temp => temp.patient.PatientId, treatmentRecord => treatmentRecord.PatientId, (temp, treatmentRecord) => new { temp.patient, temp.medicalHistory, treatmentRecord })
            .Where(joinedData => joinedData.medicalHistory.MedicalCondition == "Hypertension")
            .GroupBy(joinedData => joinedData.treatmentRecord.TreatmentType)
            .Select(treatmentGroup => new PatientSuccessfulTreatmentsViewModel
            {
                TreatmentName = treatmentGroup.Key,
                TotalTreatments = treatmentGroup.Count(),
                SuccessfulTreatments = treatmentGroup.Count(t => t.treatmentRecord.Outcome.Equals("Successful")),
                SuccessRate = (double)treatmentGroup.Count(t => t.treatmentRecord.Outcome.Equals("Successful")) / treatmentGroup.Count(),
                MedicalCondition = "Hypertension"
            })
            .ToList();

        return View(treatmentSuccessRates);
    }
}
