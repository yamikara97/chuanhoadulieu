using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chuanhoafile.Models
{
    public class RegisterViewmodel 
    {
        public Guid id { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Passwords must have at least one non alphanumeric character, Passwords must have at least one lowercase ('a'-'z'), Passwords must have at least one uppercase ('A'-'Z')")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Không khớp.")]
        public string ConfirmPassword { get; set; }

        [Phone]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Họ tên")]
        public string FullName { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public byte[] Assignment { get; set; }
    }
}
