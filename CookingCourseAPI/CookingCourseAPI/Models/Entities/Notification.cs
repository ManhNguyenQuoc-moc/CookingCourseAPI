﻿namespace CookingCourseAPI.Models.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

}
