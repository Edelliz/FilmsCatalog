using System;

namespace FilmsCatalog.Models
{
    public class Movie
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string PosterPath { get; set; } = null;
        public User AddedByUser { get; set; }
        public Guid MovieDirectorId { get; set; }
        public virtual MovieDirector MovieDirector { get; set; }
    }
}
