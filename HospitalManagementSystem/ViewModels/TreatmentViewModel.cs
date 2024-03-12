namespace HospitalManagementSystem.ViewModels
{
    /// <summary>
    /// Represents a view model for displaying information about a specific treatment type and associated patient details.
    /// </summary>
    public class TreatmentViewModel
    {
        public string? TreatmentType { get; set; }
        public List<PatientInfoViewModel>? Patients { get; set; }
    }
}