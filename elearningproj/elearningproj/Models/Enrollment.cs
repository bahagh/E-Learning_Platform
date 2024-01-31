using Microsoft.AspNetCore.Identity;

namespace elearningproj.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }

        // Navigation properties
        public virtual IdentityUser User { get; set; }
        public virtual Course Course { get; set; }
    }
}
