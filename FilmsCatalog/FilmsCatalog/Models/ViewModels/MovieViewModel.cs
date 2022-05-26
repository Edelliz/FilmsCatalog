using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace FilmsCatalog.Models.ViewModels
{
    public class MovieViewModel
    {
        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите год")]
        [Range(1700, 2030, ErrorMessage = "Введите корректный год")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Выберите режиссера")]
        public Guid MovieDirectorId { get; set; }

        public IFormFile Poster { get; set; } = null;
    }
}
