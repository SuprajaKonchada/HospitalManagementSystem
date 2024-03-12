namespace HospitalManagementSystem.ViewModels
{
    /// <summary>
    /// Represents a view model for information about patients with unsuccessful treatments.
    /// </summary>
    public class PatientUnSuccessfulTreatmentsViewModel
    {
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? TreatmentType { get; set; }
        public int UnsuccessfulCount { get; set; }
    }

}
