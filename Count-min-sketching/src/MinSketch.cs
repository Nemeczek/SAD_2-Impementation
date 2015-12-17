using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Rating = System.Tuple<long,double>;

namespace SAD2.Skething
{
	class MinSketch
	{
		static void Main(string[] args)
		{
			if (args.Length < 1) {
				Console.WriteLine("Yeah, I'm gonna need a file name to go with that..");
				return;
			}


			char[] commaSep = new char[] {','};

			IEnumerable<Rating> ratings = File.ReadLines(args[0])
				.Select(line => {
					string[] l = line.Split(commaSep);
 					//(movieId, rating) pair
					return Tuple.Create(long.Parse(l[0]),
							    double.Parse(l[1]));
				});

			IEnumerable<Rating> averages = EstimatedAverages(ratings).OrderByDescending(r => r.Item2);
			foreach (Rating r in averages) {
				Console.WriteLine("{0},{1}", r.Item1, r.Item2);
			}
		}

		private static IEnumerable<Rating> EstimatedAverages(IEnumerable<Rating> ratings) {
			HashSet<long> seen = new HashSet<long>();
			Sketch sumSketch = new Sketch(0.1, 0.01, 99999);
			Sketch frequencySketch = new Sketch(sumSketch);
			ratings.ForEach(r => {
				//Console.WriteLine(r.Item1 + " : " + r.Item2);
				seen.Add(r.Item1);
				sumSketch.Add(r); //sum of ratings
				frequencySketch.Add(Tuple.Create(r.Item1, 1.0)); //freq of ratings
			});
			List<Rating> avgs = new List<Rating>();
			foreach(long i in seen){
				double sum = sumSketch.Get(i);
				double freq = frequencySketch.Get(i);
					avgs.Add(Tuple.Create(i, sum/freq));
			}
			return avgs;
		}
	}

	static class Extensions {
		public static void ForEach<T>(this System.Collections.Generic.IEnumerable<T> l, System.Action<T> f) {
			foreach (T x in l) {
				f(x);
			}
		}
	}
}
