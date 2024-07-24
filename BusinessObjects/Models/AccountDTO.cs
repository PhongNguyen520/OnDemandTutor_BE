using BusinessObjects.Models.TutorModel;
using System;
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
    
        public string? Avatar { get; set; }

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
        public string? Avatar { get; set; }
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
        public string? Avatar { get; set; }
        public int isActive { get; set; } = 1;
    }

    public class TutorVM
    {
        public string FullName { get; set; } = null!;

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string? Avatar { get; set; }

        public DateTime Dob { get; set; }

        public string Education { get; set; } = null!;

        public string TypeOfDegree { get; set; } = null!;

        public string CardId { get; set; }

        public float HourlyRate { get; set; }

        public string? Photo { get; set; }

        public string? Headline { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreateDay { get; set; }

        public string TutorId { get; set; }

    }

    public class StudentVM
    {
        public string FullName { get; set; } = null!;

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string? Avatar { get; set; }

        public string? SchoolName { get; set; }

        public string? Address { get; set; }

        public int? Age { get; set; }

        public bool IsParent { get; set; }

        public DateTime CreateDay { get; set; }

    }

    public class RefreshTokenVM
    {
        public string refreshToken { get; set; }
        public string userId { get; set; }

    }

    public class SignUpModerator
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
        public string? Avatar { get; set; }
        public int isActive { get; set; }

    }

    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }

    public class TutorInterVM
    {
        public string AccountId { get; set; }
        public string FullName { get; set; } = null!;

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string? Avatar { get; set; }

        public DateTime Dob { get; set; }

        public string Education { get; set; } = null!;

        public string TypeOfDegree { get; set; } = null!;

        public string CardId { get; set; }

        public float HourlyRate { get; set; }

        public string? Photo { get; set; }

        public string? Headline { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreateDay { get; set; }

        public string TutorId { get; set; }

        public bool AccountIsActive { get; set;}

    }

    public class Student10VM
    {
        public string AccountId { get; set; }
        public string FullName { get; set; } = null!;

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string? Avatar { get; set; }

        public string? SchoolName { get; set; }

        public string? Address { get; set; }

        public int? Age { get; set; }

        public bool IsParent { get; set; }

        public DateTime CreateDay { get; set; }

        public string StudentId { get; set; }

        public bool AccountIsActive { get; set; }

    }

    public class AccountTutorAdVM
    {
        public string AccountId { get; set; }
        public string FullName { get; set; } = null!;

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string? Avatar { get; set; }

        public DateTime Dob { get; set; }

        public string Education { get; set; } = null!;

        public string TypeOfDegree { get; set; } = null!;

        public string CardId { get; set; }

        public float HourlyRate { get; set; }

        public string? Photo { get; set; }

        public string? Headline { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreateDay { get; set; }

        public string TutorId { get; set; }

        public List<TutorAdsModel> TutorAds { get; set; }
    }

    public class RequestWithdrawMoneyVM
    {
        public string Password { get; set; }
        public float Amount { get; set; }
        public int Type {  get; set; }
    }

    public class PaymentTransactionVM
    {
        public string Id { get; set; } 
        public float? Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? TranDate { get; set; }

        public int? Type { get; set; }
        public bool? IsValid { get; set; }
        public string? WalletId { get; set; } 
        public string? PaymentDestinationId { get; set; }
        public float? Balance { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public string AccountId { get; set; }
    }

    public class RequestDrawVM
    {
        public string idTran { get; set; }
        public bool Status { get; set; }

        public float Amount { get; set; }
    }

    public class RefundStudentVM
    {
        public string StudentId { get; set; }
        public float Amount { get; set; }
    }
}
