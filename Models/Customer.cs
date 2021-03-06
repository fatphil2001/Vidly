﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please Enter *")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }
        
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Min18YearsIfAMember(nameof(MembershipTypeId))]
        public DateTime? DateOfBirth { get; set; }
    }
}