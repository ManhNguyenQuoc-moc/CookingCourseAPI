namespace CookingCourseAPI.DTOs
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
        public bool IsLocked { get; set; }

    }

}
