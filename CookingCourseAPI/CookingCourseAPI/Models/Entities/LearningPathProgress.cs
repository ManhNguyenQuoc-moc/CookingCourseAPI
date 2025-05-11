namespace CookingCourseAPI.Models.Entities
{
    public class LearningPathProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // ID người dùng
        public int LearningPathId { get; set; }  // Mã lộ trình học
        public bool IsPathCompleted { get; set; }  // Trạng thái hoàn thành lộ trình học
        public DateTime LastUpdated { get; set; }  // Thời gian cập nhật
    }
}
