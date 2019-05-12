using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PManager.Models
{
    public class RegisterModels
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập Email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ, vui lòng kiểm tra lại.")]
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
    }

    public class LoginModels
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập Email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ, vui lòng kiểm tra lại.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Ghi nhớ lựa chọn của tôi.")]
        public bool Rememberme { get; set; }

        public string ReturnUrl { get; set; }
    }
}
