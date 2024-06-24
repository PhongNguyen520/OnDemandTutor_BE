﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class AccountDTO
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [StringLength(10)]
        public String? PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public int isActive { get; set; } = 1;
        public bool isAdmin { get; set; }
    
    }

    public class UserRoles : Account
    {
        public List<string> RolesName { get; set; }
    }
    public class UserRolesVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [StringLength(10)]
        public String? PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public int isActive { get; set; } = 1;
        public List<string> RolesName { get; set; }
    }

    public class UserSignIn
    {
        public String UserName { get; set; }
        public String Password { get; set; }
    }

    public class UserSignInVM : UserSignIn
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class UserVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        [StringLength(10)]
        public String? PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public int isActive { get; set; } = 1;
    }

    public class TutorVM
    {
        public string FullName { get; set; } = null!;

        public bool Gender { get; set; }

        public int PhoneNumber { get; set; }

        public string Email { get; set; }

        public string? Avatar { get; set; }

        public DateTime Dob { get; set; }

        public string Education { get; set; } = null!;

        public string TypeOfDegree { get; set; } = null!;

        public int CardId { get; set; }

        public float HourlyRate { get; set; }

        public string? Photo { get; set; }

        public string? Headline { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }

    }

    public class RefreshTokenVM
    {
        public string refreshToken { get; set; }
        public string userId { get; set; }
    }

}
