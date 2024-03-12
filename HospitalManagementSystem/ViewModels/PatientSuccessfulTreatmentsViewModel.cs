namespace HospitalManagementSystem.ViewModels
{
    /// <summary>
    /// Represents a view model for successful treatments information for a patient.
    /// </summary>
    public class PatientSuccessfulTreatmentsViewModel
    {
        public string? TreatmentName { get; set; }
        public int TotalTreatments { get; set; }
        public int SuccessfulTreatments { get; set; }
        public double SuccessRate { get; set; }
        public string? MedicalCondition { get; set; }
    }
}