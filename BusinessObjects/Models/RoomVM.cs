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
        public string ConversationId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long", MinimumLength = 5)]
        [RegularExpression(@"^\w+( \w+)*$", ErrorMessage = "Characters allowed: letters, numbers, and one space between words")]
        public string AccountId { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }

    public class MessageVM
    {
        public string MessageId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string Avatar { get; set; }
        public string Time { get; set; }
        public string RoomId { get; set; }
    }

    public class UploadViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserConnection
    {
        public string UserName { get; set; } = string.Empty;
        public string ChatRoom { get; set; } = string.Empty;
    }

    public class CreateMessage
    {
        public string RoomId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
