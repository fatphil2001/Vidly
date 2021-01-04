using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.ViewModels;

namespace Vidly.Models
{
    public class NoMoviesBefore1800Attribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (MovieFormViewModel)validationContext.ObjectInstance;

            if (movie.ReleaseDate == null)
            {
                return ValidationResult.Success;
                // return new ValidationResult("Release date is required.");
            }

            if ( movie.ReleaseDate >= new DateTime(1800,1,1))
                return ValidationResult.Success;

            return new ValidationResult("Release date must be later that 1/1/1800.");
        }
    }
}