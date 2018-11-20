using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace LDSMovies.Models
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new LDSMoviesContext(
				serviceProvider.GetRequiredService<DbContextOptions<LDSMoviesContext>>()))
			{
				// Look for any movies.
				if (context.Movie.Any())
				{
					return;   // DB has been seeded
				}

				context.Movie.AddRange(
					 new Movie
					 {
						 Title = "Once I Was a Beehive",
						 ReleaseDate = DateTime.Parse("2015-8-14"),
						 Genre = "Comedy",
						 Rating = "PG",
						 Price = 7.99M
					 },

					 new Movie
					 {
						 Title = "17 Miracles",
						 ReleaseDate = DateTime.Parse("2011-6-3"),
						 Genre = "Comedy",
						 Rating = "PG",
						 Price = 8.99M
					 },

					 new Movie
					 {
						 Title = "Meet The Mormons",
						 ReleaseDate = DateTime.Parse("2014-10-10"),
						 Genre = "Comedy",
						 Rating = "PG",
						 Price = 9.99M
					 },

				   new Movie
				   {
					   Title = "Pride and Prejudice: A Romantic Comedy",
					   ReleaseDate = DateTime.Parse("2003-12-5"),
					   Genre = "Romantic Comedy",
					   Rating = "PG",
					   Price = 3.99M
				   },

				   new Movie
				   {
					   Title = "Test",
					   ReleaseDate = DateTime.Parse("2003-12-5"),
					   Genre = "Romantic Comedy",
					   Rating = "PG",
					   Price = 3.99M
				   }
				);
				context.SaveChanges();
			}
		}
	}
}