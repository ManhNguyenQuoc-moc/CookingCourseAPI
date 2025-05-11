namespace CookingCourseAPI.Models.Entities
{
    public class LearningPathCourse
    {
        public int LearningPathId { get; set; }
        public LearningPath LearningPath { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
