using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FilmsCatalog.Data;
using FilmsCatalog.Models;
using FilmsCatalog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using FilmsCatalog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;

namespace FilmsCatalog.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IFileUploadService _fileUploadService;

        public MoviesController(ApplicationDbContext context, UserManager<User> userManager, IFileUploadService fileUploadService)
        {
            _context = context;
            _userManager = userManager;
            _fileUploadService = fileUploadService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var movies = _context.Movies.Include(x => x.MovieDirector).OrderBy(x => x.Year);
            var count = await movies.CountAsync();
            var items = await movies.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var pageInfo = new PageViewModel(count, page, pageSize);
            var moviesInfo = new MoviesPageViewModel
            {
                Movies = items,
                PageInfo = pageInfo
            };

            return View(moviesInfo);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            var movie = await _context.Movies
                .Include(x => x.AddedByUser)
                .Include(x => x.MovieDirector)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (movie == default)
            {
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Account", new { returnUrl = Request.Path });
            }
            ViewData["MovieDirectors"] = new SelectList(_context.MovieDirectors, "Id", "FullName");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                string posterPath = null;
                if (model.Poster != null)
                {
                    posterPath = await _fileUploadService.UploadImageAsync(model.Poster);
                }

                var movie = new Movie
                {
                    Name = model.Name,
                    Description = model.Description,
                    Year = model.Year,
                    PosterPath = posterPath,
                    MovieDirectorId = model.MovieDirectorId,
                    AddedByUser = user
                };

                _context.Add(movie);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieDirectors"] = new SelectList(_context.MovieDirectors, "Id", "FullName", model.MovieDirectorId);
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Account", new { returnUrl = Request.Path });
            }

            var movie = await _context.Movies
                .Include(x => x.MovieDirector)
                .Include(x => x.AddedByUser)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (movie == default)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (movie.AddedByUser.Id != user.Id)
            {
                return Forbid();
            }

            ViewData["MovieDirectors"] = new SelectList(_context.MovieDirectors, "Id", "FullName", movie.MovieDirectorId);
            var model = new MovieViewModel
            {
                Name = movie.Name,
                Description = movie.Description,
                Year = movie.Year,
                MovieDirectorId = movie.MovieDirectorId
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MovieViewModel model)
        {
            var movie = await _context.Movies
                .Include(x => x.MovieDirector)
                .Include(x => x.AddedByUser)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (movie == default)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (movie.AddedByUser.Id != user.Id)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                if (model.Poster != null)
                {
                    if (movie.PosterPath != null)
                    {
                        _fileUploadService.DeleteFile(movie.PosterPath);
                    }

                    movie.PosterPath = await _fileUploadService.UploadImageAsync(model.Poster);
                }

                movie.Name = model.Name;
                movie.Description = model.Description;
                movie.Year = model.Year;
                movie.MovieDirectorId = model.MovieDirectorId;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id });
            }
            ViewData["MovieDirectors"] = new SelectList(_context.MovieDirectors, "Id", "FullName", model.MovieDirectorId);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<Guid> CreateMovieDirector(string name, string surname, string patronymic)
        {
            var movieDirector = new MovieDirector
            {
                Name = name,
                Surname = surname,
                Patronymic = patronymic
            };

            _context.Add(movieDirector);
            await _context.SaveChangesAsync();

            return movieDirector.Id;
        }
    }
}
