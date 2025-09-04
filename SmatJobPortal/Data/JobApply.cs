using System.ComponentModel.DataAnnotations;

namespace SmatJobPortal.Data
{
    public class JobApply
    {
        public int Id { get; set; }

  
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string CoverLetter { get; set; }
        public string Portfolio { get; set; }

        public DateTime AppliedAt { get; set; } = DateTime.Now;

        public int JobId { get; set; }
        public Job Job { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string ResumePath { get; set; }

        public JobApplyStatus Status { get; set; } = JobApplyStatus.New;

    }

    public enum JobApplyStatus
    {
        New,
        Reviewed,
        Interview,
        Accepted,
        Rejected,
        Withdrawn
    }
}
