using System.ComponentModel.DataAnnotations;

namespace SmatJobPortal.Data
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public String JobType { get; set; }
        [Required]
        public String JobCategory { get; set; }
        [Required]
        public String JobLocation { get; set; }
        public string SalaryRange { get; set; }
        [Required]
        public string JobDescription { get; set; }
        [Required]
        public string Requirements { get; set; }
        public string Benefits { get;set; }

        public DateTime ApplicationDeadline { get; set; }

        [Required]
        public ApplicationUser EmployerUser { get; set; }

        
        public DateTime AddedTime { get; set; }=DateTime.Now;
    }
  
}
