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
			string inputFile = null;
			string outputFileName = null;

			ManageConsoleInput(args,ref inputFile,ref outputFileName);

			//var output = GeneralApproach(@"C:\Users\nemec\Downloads\ml-latest\ratings.csv");
			var output = GeneralApproach(inputFile, outputFileName);
			var something = output.Select(n => n.Rating / n.Occurences).OrderByDescending(v => v).ToList();
			//var results = ReadAveragesFromFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Averages.txt");

			var bb = something.ToList();
			bb.Reverse();

			Console.WriteLine(KendallTauDistance.KendallTau.Distance(something.ToArray(), bb.ToArray()));
			Console.ReadKey();
		}

		private static IEnumerable<Movie> GeneralApproach(string pathToFile,string outputFileName = null, string outputFilePath = null)
		{
			if (outputFileName == null)
				outputFileName = "Averages.txt";	

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
			currentMoviesList.Select(d => d.Value).OrderByDescending(n => n.Rating / n.Occurences).SaveToFile(outputFileName,outputFilePath);
			return currentMoviesList.Select(d => d.Value);
		}

		private static void ManageConsoleInput(string[] args,ref string inputFile,ref string outputFileName)
		{
			if (args[0] == null)
			{
				Console.WriteLine("This program needs at least one parameter - input file");
				throw new ApplicationException();
			}

			inputFile = args[0];

			if (args[1] != null)
				outputFileName = args[1];
		}
	}
}
