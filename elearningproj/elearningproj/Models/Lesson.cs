using System.ComponentModel.DataAnnotations.Schema;

namespace elearningproj.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public LessonType Type { get; set; }

        public int CourseId { get; set; }
    }

    public enum LessonType
    {
        TextFile,
        PPT,
        Image,
        Video,
        PDF
    }

}
