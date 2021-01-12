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

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [NoMoviesBefore1800]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Current Stock")]
        public short? CurrentStock { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Available Stock")]
        public short? AvailableStock { get; set; }

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
                AvailableStock = movie.NumberAvailable;
                GenreId = movie.GenreId;
            }
            else
                Id = 0;
        }

        public static void PopulateMovieFromViewModel(MovieFormViewModel movieFormViewModel, Movie movie)
        {
            movie.Name = movieFormViewModel.Name;
            movie.ReleaseDate = movieFormViewModel.ReleaseDate.Value;
            movie.GenreId = movieFormViewModel.GenreId.Value;
            movie.CurrentStock = movieFormViewModel.CurrentStock.Value;
            movie.NumberAvailable = movieFormViewModel.AvailableStock.Value;
        }

    }
}