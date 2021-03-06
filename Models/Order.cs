﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication7.Models
{
    public class Order
    {
        [BindNever]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<Cart.CartLine> Lines { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        [Required(ErrorMessage = @"Nhập tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = @"Nhập địa chỉ thứ 1")]
        public string Line1 { get; set; }

        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = @"Nhập City")]
        public string City { get; set; }

        [Required(ErrorMessage = @"Nhập state")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = @"Nhập Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = @"Email")]
        public string Email { get; set; }
    }
}