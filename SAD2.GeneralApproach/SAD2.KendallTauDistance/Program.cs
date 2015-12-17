using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SAD2.KendallTauDistance
{
	class Program
	{
		static void Main(string[] args)
		{
			List<decimal> a = new List<decimal>()
			{
				1.0M  , 2.0M ,  3.0M  , 4.0M  , 5.0M
			};

			List<decimal> b = new List<decimal>()
			{
				3.0M  , 4.0M ,  1.0M ,  2.0M  , 5.0M
			};

			Console.WriteLine(KendallTau.Distance(a.ToArray(),b.ToArray()));
			Console.ReadKey();
		}
	}

	class KendalTauMovieRatings
	{
		static void Main(string[] args)
		{
			if(args.Length != 2) {
				Console.WriteLine("program takes two arguments -- two file name");
				return;

			}

			char[] commaSep = new char[] {','};
			decimal[] l1 = File.ReadLines(args[0]).Select(line=> {
					string[] l = line.Split(commaSep);
 					//(movieId, rating) pair
					return decimal.Parse(l[0]);
				}).ToArray();

			decimal[] l2 = File.ReadLines(args[1]).Select(line=> {
					string[] l = line.Split(commaSep);
 					//(movieId, rating) pair
					return decimal.Parse(l[0]);
				}).ToArray();

			Console.WriteLine(KendallTau.Distance(l1,l2));
		}
	}
}
