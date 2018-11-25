using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LDSMovies.Models
{
	public class MovieReleaseDateViewModel
	{
		public List<Movie> Movies;
		public SelectList ReleaseDate;
		public string MovieReleaseDate { get; set; }
		public string SearchString { get; set; }
	}
}
