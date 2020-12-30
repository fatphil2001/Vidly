using System.ComponentModel.DataAnnotations;
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
        [Required]
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ReleaseDate{ get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime AddedDate{ get; set; }
        [Required]
        public short CurrentStock { get; set; }
    }
}