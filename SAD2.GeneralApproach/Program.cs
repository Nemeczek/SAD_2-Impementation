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
			var output = GeneralApproach(@"C:\Users\nemec\Downloads\ml-latest\ratings.csv");
			var something = output.Select(n => n.Rating / n.Occurences).OrderByDescending(v => v).ToList();
			var results = ReadAverages(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Averages.txt");

			Console.WriteLine(KendallTauDistance.KendallTau.Distance(something.Take(100).ToArray(), results.Select(v => v.Item2).Take(100).ToArray()));
			Console.ReadKey();
		}

		private static IEnumerable<Movie> GeneralApproach(string pathToFile)
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

			string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var output = currentMoviesList.Select(d => d.Value).OrderByDescending(n => n.Rating/n.Occurences);

			using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\Averages.txt"))
			{
				foreach (var movie in output)
					outputFile.WriteLine(movie.ToString());
			}

			return currentMoviesList.Select(d => d.Value);
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
