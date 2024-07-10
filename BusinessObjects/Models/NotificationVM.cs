using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class NotificationVM
    {
        public string FullName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string AccountId { get; set; } = string.Empty;
    }

    public class NotiListVM
    {
        public string FullName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Avatar {  get; set; }
        public string CreateDay { get; set; } = string.Empty;
    }

    public class CreateNotiVM
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string AccountId { get; set; } = string.Empty;
    }
}
