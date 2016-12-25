using System;

namespace Core.Entities
{
    public class User
    {
        public int? ID { get; set; }
        public int? RoleID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateTime { get; set; }

        public Role Role { get; set; }

        public User()
        {
            CreateTime = DateTime.Now;
        }
    }
}
