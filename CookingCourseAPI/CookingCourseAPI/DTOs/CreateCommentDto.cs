﻿namespace CookingCourseAPI.DTOs
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public int BlogId { get; set; }
        public int? ParentCommentId { get; set; } = 0;
        public int UserId { get; set; } // hoặc lấy từ token
    }

}
