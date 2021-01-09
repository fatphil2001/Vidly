using System;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte GenreId { get; set; }
        public GenreDto Genre { get; set; }

        [NoMoviesBefore1800]
        public DateTime ReleaseDate{ get; set; }
        public DateTime AddedDate{ get; set; }
        public short CurrentStock { get; set; }
    }
}