namespace CookingCourseAPI.DTOs
{
    public class CourseCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public decimal Price { get; set; }

     
        public bool IsFree { get; set; }

        public List<VideoDto> Videos { get; set; }
    }
}
