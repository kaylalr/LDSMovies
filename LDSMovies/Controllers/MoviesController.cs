using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LDSMovies.Models;

namespace LDSMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly LDSMoviesContext _context;

        public MoviesController(LDSMoviesContext context)
        {
            _context = context;
        }

		//I have no idea what this is doing or why we need it
		[HttpPost]
		public string Index(string searchString, bool notUsed)
		{
			return "From [HttpPost]Index: filter on " + searchString;
		}


		// GET: Movies
		// Requires using Microsoft.AspNetCore.Mvc.Rendering;
		public async Task<IActionResult> Index(string movieGenre, string searchString, string movieReleaseDate)
		{
			// Use LINQ to get list of genres.
			IQueryable<string> genreQuery = from m in _context.Movie
											orderby m.Genre
											select m.Genre;

			IQueryable<DateTime> releaseDateQuery = from m in _context.Movie
											orderby m.ReleaseDate
											select m.ReleaseDate;

			var movies = from m in _context.Movie
						 select m;

			if (!String.IsNullOrEmpty(searchString))
			{
				movies = movies.Where(s => s.Title.Contains(searchString));
			}

			if (!String.IsNullOrEmpty(movieGenre))
			{
				movies = movies.Where(x => x.Genre == movieGenre);
			}

			if (!String.IsNullOrEmpty(movieReleaseDate))
			{
				movies = movies.Where(x => x.ReleaseDate.ToString() == movieReleaseDate);
			}

			//if (movieReleaseDate != null)
			//{
			//	movies = movies.Where(x => x.ReleaseDate == movieReleaseDate);
			//}

			//if (!String.IsNullOrEmpty(movieReleaseDate.ToString()))
			//{
			//	if (movieReleaseDate.ToString() == "asc")
			//	{
			//		//Console.WriteLine("working");
			//	}
			//	movies = movies.Where(x => x.ReleaseDate == movieReleaseDate);
			//}

			var movieGenreVM = new MovieGenreViewModel();
			movieGenreVM.Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
			movieGenreVM.ReleaseDate = new SelectList(await releaseDateQuery.Distinct().ToListAsync());
			movieGenreVM.Movies = await movies.ToListAsync();
			movieGenreVM.SearchString = searchString;

			return View(movieGenreVM);
		}

		// GET: Movies/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
