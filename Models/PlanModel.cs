namespace PatientReport.Models
{
    public class PlanModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CourseId { get; set; }
        public string ApprovalStatus { get; set; }
        public string Approver { get; set; }
        public string ApprovalHistory { get; set; }
    }
}
