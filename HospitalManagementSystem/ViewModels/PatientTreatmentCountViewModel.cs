namespace HospitalManagementSystem.ViewModels
{
    /// <summary>
    /// Represents a view model for counting occurrences of treatments for each patient.
    /// </summary>
    public class PatientTreatmentCountViewModel
    {
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? TreatmentType { get; set; }
        public int Count { get; set; }
    }
}
