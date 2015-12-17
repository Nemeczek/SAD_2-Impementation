using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Rating = System.Tuple<long,double>;

namespace SAD2.GeneralApproach
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("This program needs exactly one parameter - input file");
				return;
			}

			IEnumerable<Movie> moviesByAvg = GeneralApproach(File.ReadLines(args[0]));

			foreach(Movie m in moviesByAvg){
				Console.WriteLine(m.Id + "," +m.Rating/m.Occurences);
			}
			//Console.WriteLine(KendallTauDistance.KendallTau.Distance(something.ToArray(), bb.ToArray()));
		}

		private static IEnumerable<Movie> GeneralApproach(IEnumerable<string> lines)
		{
			Dictionary<long, Movie> currentMoviesList = new Dictionary<long, Movie>();
			foreach(string line in lines)
			{
				var parts = line.Split(",".ToCharArray());
				var movie = new Movie(long.Parse(parts[0]), decimal.Parse(parts[1]));

				if (currentMoviesList.ContainsKey(movie.Id))
					currentMoviesList[movie.Id].Rating += movie.Rating;
				else
					currentMoviesList.Add(movie.Id, movie);

				currentMoviesList[movie.Id].Occurences++;
			}
			return currentMoviesList.Select(d => d.Value).OrderByDescending(n => n.Rating / n.Occurences);
		}
	}
}
