namespace HospitalManagementSystem.ViewModels
{
    /// <summary>
    /// Represents a view model for success rates of a specific medical procedure, such as surgery.
    /// </summary>
    public class SuccessRateViewModel
    {
        public int SurgeryCount { get; set; }
        public int SuccessfulSurgeryCount { get; set; }
        public double SuccessRate { get; set; }
    }
}
