using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drone.DAL.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        // public bool IsActive { get; set; } // Trạng thái tài khoản (đang hoạt động hay không)
    }
}
