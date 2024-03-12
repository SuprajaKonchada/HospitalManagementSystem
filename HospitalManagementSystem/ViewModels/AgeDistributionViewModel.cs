namespace HospitalManagementSystem.ViewModels
{
    /// <summary>
    /// Represents a view model for age distribution based on patients' gender and medical condition.
    /// </summary>
    public class AgeDistributionViewModel
    {
        public string? Gender { get; set; }
        public double AverageAge { get; set; }
        public string? MedicalCondition { get; set; }
    }

}
