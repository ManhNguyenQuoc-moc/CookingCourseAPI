namespace CookingCourseAPI.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Thêm trường giá
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        // Thêm trường trạng thái: miễn phí hay có phí
        public bool IsFree { get; set; }
        //public ICollection<Rating> Ratings { get; set; }
        public ICollection<CourseVideo> Videos { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

}
