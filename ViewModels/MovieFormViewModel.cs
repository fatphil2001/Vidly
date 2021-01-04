using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public byte? GenreId { get; set; }

        [Required(ErrorMessage = "REQ")]
        [Display(Name = "Released")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [NoMoviesBefore1800(ErrorMessage = "Et voila")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Current Stock")]
        public short? CurrentStock { get; set; }


        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            if (movie != null)
            {
                Id = movie.Id;
                Name = movie.Name;
                ReleaseDate = movie.ReleaseDate;
                CurrentStock = movie.CurrentStock;
                GenreId = movie.GenreId;
            }
            //else
            //    Id = 0;
        }

    }
}