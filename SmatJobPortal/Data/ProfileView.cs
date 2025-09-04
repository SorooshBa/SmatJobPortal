using System.ComponentModel.DataAnnotations;

namespace SmatJobPortal.Data
{
    public class ProfileView
    {
        public int Id { get; set; }
        [Required]
        public string ViewedUserId { get; set; }
        public ApplicationUser ViewedUser { get; set; }
        [Required]
        public string ViewerUserId { get; set; }
        public ApplicationUser ViewerUser { get; set; }
        public DateTime ViewedAt { get; set; } = DateTime.Now;
    }
}


