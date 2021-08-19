using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chuanhoafile.Models
{
    public class ResetPassword
    {
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Mật khẩu phải chứa ít nhất 1 ký tự in hoa, kí tự số  và kí tự đặc biệt")]
        [DataType(DataType.Password)]
        [Display(Name = "Mât khẩu mới")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("Password", ErrorMessage = "Không khớp.")]
        public string ConfirmPassword { get; set; }
    }
}
