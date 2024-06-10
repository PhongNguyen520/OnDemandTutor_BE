using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class RoomVM
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long", MinimumLength = 5)]
        [RegularExpression(@"^\w+( \w+)*$", ErrorMessage = "Characters allowed: letters, numbers, and one space between words")]
        public string Name { get; set; }
    }

    public class MessageVM
    {
        [Required]
        public string Content { get; set; }
        public string Time { get; set; }
        public string From { get; set; }
        [Required]
        public string Room { get; set; }
        public string Avatar { get; set; }
    }

    public class UploadViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserChatVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string CurrentRoom { get; set; }
        public string Device { get; set; }
    }
}
