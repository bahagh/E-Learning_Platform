using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using elearningproj.Data;
using elearningproj.Models;

namespace elearningproj.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllCourses", Name = "GetAllCourses")]
        public IActionResult GetAllCourses()
        {
            var courses = _context.Courses.ToList();
            return Ok(courses);
        }

        [HttpGet("GetSpecificCourse/{id}", Name = "GetCourseById")]
        public IActionResult GetCourseById(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost("CreateCourse", Name = "CreateCourse")]
        public IActionResult CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
        }

        [HttpPut("UpdateSpecificCourse/{id}", Name = "UpdateCourseById")]
        public IActionResult UpdateCourseById(int id, [FromBody] Course updatedCourse)
        {
            var existingCourse = _context.Courses.Find(id);

            if (existingCourse == null)
            {
                return NotFound();
            }

            existingCourse.Title = updatedCourse.Title;
            existingCourse.Description = updatedCourse.Description;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("DeleteSpecificCourse/{id}", Name = "DeleteCourseById")]
        public IActionResult DeleteCourseById(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("{courseId}/lessons", Name = "GetLessonsByCourseId")]
        public IActionResult GetLessonsByCourseId(int courseId)
        {
            var lessons = _context.Lessons.Where(l => l.CourseId == courseId).ToList();
            return Ok(lessons);
        }

        [HttpPost("{courseId}/add-lesson", Name = "AddLessonToCourse")]
        public IActionResult AddLessonToCourse(int courseId, Lesson lesson)
        {
            var course = _context.Courses.Find(courseId);

            if (course == null)
            {
                return NotFound("Course not found");
            }

            lesson.CourseId = courseId;
            _context.Lessons.Add(lesson);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetLessonsByCourseId), new { courseId = courseId }, lesson);
        }

        [HttpPut("{courseId}/update-lesson/{lessonId}", Name = "UpdateLessonInCourse")]
        public IActionResult UpdateLessonInCourse(int courseId, int lessonId, [FromBody] Lesson updatedLesson)
        {
            var existingLesson = _context.Lessons.FirstOrDefault(l => l.Id == lessonId && l.CourseId == courseId);

            if (existingLesson == null)
            {
                return NotFound("Lesson not found in the course");
            }

            existingLesson.Title = updatedLesson.Title;
            existingLesson.Content = updatedLesson.Content;
            existingLesson.Type = updatedLesson.Type;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{courseId}/delete-lesson/{lessonId}", Name = "DeleteLessonInCourse")]
        public IActionResult DeleteLessonInCourse(int courseId, int lessonId)
        {
            var lesson = _context.Lessons.FirstOrDefault(l => l.Id == lessonId && l.CourseId == courseId);

            if (lesson == null)
            {
                return NotFound("Lesson not found in the course");
            }

            _context.Lessons.Remove(lesson);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
