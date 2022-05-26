using System;
using System.Collections.Generic;

namespace FilmsCatalog.Models
{
    public class MovieDirector
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }

        public string FullName
        {
            get { return $"{Name} {Surname} {Patronymic}"; }
        }
    }
}
