using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DPizza.Models.Data
{
    public class ValidateUser
    {

        [Display(Name = "รหัส")]
        public int UserId { get; set; }

        [Display(Name = "คำนำหน้าชื่อ")]
        public int? TitleId { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "ชื่อผู้ใช้")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "นามสกุล")]
        public string UserLastname { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "อีเมลล์")]
        [EmailAddress(ErrorMessage = "โปรดกรอกชื่อผู้ใช้รูปแบบอีเมล์")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "รหัสผ่าน")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [Display(Name = "ที่อยู่")]
        public string UserAddress { get; set; }

    }
    [ModelMetadataType(typeof(ValidateUser))]
    public partial class User 
    { 
    }

}
