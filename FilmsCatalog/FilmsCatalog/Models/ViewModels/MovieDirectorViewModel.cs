using System;
using System.ComponentModel.DataAnnotations;

namespace FilmsCatalog.Models.ViewModels
{
    public class MovieDirectorViewModel
    {
        public Guid? Id { get; set; } = null;

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string Surname { get; set; }

        public string Patronymic { get; set; } = null;
    }
}
