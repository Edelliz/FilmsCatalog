using System.Collections.Generic;

namespace FilmsCatalog.Models.ViewModels
{
    public class MoviesPageViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        public PageViewModel PageInfo { get; set; }
    }
}
