﻿using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)] 
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Released")]
        public DateTime ReleaseDate{ get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Added")]
        public DateTime AddedDate{ get; set; }
        
        [Required]
        [Display(Name = "Current Stock")]
        public short CurrentStock { get; set; }
    }
}