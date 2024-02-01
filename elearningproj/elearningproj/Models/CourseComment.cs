using System;

namespace elearningproj.Models
{
    public class CourseComment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public int CourseId { get; set; }
        
    }
}
