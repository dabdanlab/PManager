using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PManager.Models
{
    public class RegisterModels
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập Email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải tối đa {0} ký tự và tối thiểu {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác thực mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác thực không trùng khớp, vui lòng kiểm tra lại.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên.")]
        [Display(Name = "Tên đầy đủ")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ, vui lòng kiểm tra lại.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ.")]
        [Display(Name = "Adress")]
        public string Adress { get; set; }

        
        [Required(ErrorMessage = "Bạn chưa nhập ngày tháng năm sinh")]
        [Display(Name = "Birthday")]
        public int Birthday { get; set; }

        public string ReturnUrl { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }

    public class LoginModels
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
