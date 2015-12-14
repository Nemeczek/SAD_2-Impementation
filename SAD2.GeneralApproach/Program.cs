using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAD2.GeneralApproach
{
	class Program
	{
		static void Main(string[] args)
		{
			var output = generalApproach(@"C:\Users\nemec\Downloads\ml-latest\ratings.csv");
			Console.ReadKey();
		}

		private static IEnumerable<Decimal> generalApproach(string pathToFile)
		{
			Dictionary<long, Movie> currentMoviesList = new Dictionary<long, Movie>();
			const int bufferSize = 128;
			using (var fileStream = File.OpenRead(pathToFile))
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
			{
				string line;
				streamReader.ReadLine();
				while ((line = streamReader.ReadLine()) != null)
				{
					var parts = line.Split(",".ToCharArray());
					var movie = new Movie(long.Parse(parts[1]), decimal.Parse(parts[2]));

					if (currentMoviesList.ContainsKey(movie.Id))
						currentMoviesList[movie.Id].Rating += movie.Rating;
					else
						currentMoviesList.Add(movie.Id, movie);

					currentMoviesList[movie.Id].Occurences++;
				}
			}
			return currentMoviesList.Select(d => d.Value).Select(n => n.Rating / n.Occurences).OrderByDescending(v => v).ToList();
		}

		private static IEnumerable<Tuple<long,decimal>> ReadAverages(string path)
		{
			const int bufferSize = 128;
			Stack<Tuple<long, decimal>> averages = new Stack<Tuple<long, decimal>>();
			using (var fileStream = File.OpenRead(path))
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
			{
				string line;
				streamReader.ReadLine();
				while ((line = streamReader.ReadLine()) != null)
				{
					var parts = line.Split(",".ToCharArray());
					averages.Push(new Tuple<long, decimal>(long.Parse(parts[0]), decimal.Parse(parts[1])));
				}
			}
			return averages;
		}
	}
}
