namespace HospitalManagementSystem.ViewModels
{
    /// <summary>
    /// Represents a view model for counting occurrences of medical conditions for each patient.
    /// </summary>
    public class PatientMedicalConditionCountViewModel
    {
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? MedicalCondition { get; set; }
        public int Count { get; set; }
    }

}
