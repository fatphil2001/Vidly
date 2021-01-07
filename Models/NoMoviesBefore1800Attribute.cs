using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Dtos;
using Vidly.ViewModels;

namespace Vidly.Models
{
    public class NoMoviesBefore1800Attribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = validationContext.ObjectInstance as Movie;
            if (movie != null)
                return CheckDateLaterThan1800(movie.ReleaseDate);

            var movieDto = validationContext.ObjectInstance as MovieDto;
            if (movieDto != null)
                return CheckDateLaterThan1800(movieDto.ReleaseDate);

            var movieFormViewModel = validationContext.ObjectInstance as MovieFormViewModel;
            return CheckDateLaterThan1800(movieFormViewModel.ReleaseDate);
        }

        protected ValidationResult CheckDateLaterThan1800(DateTime? releaseDate)
        {
            if (releaseDate == null)
            {
                return new ValidationResult("Release date is required.");
            }

            if (releaseDate >= new DateTime(1800, 1, 1))
                return ValidationResult.Success;

            return new ValidationResult("Release date must be later that 1/1/1800.");
        }
    }
}