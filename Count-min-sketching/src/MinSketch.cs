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

			List<double> averages = EstimatedAverages(ratings);
			foreach (double d in averages) {
				Console.WriteLine(d);
			}
		}

		private static List<double> EstimatedAverages(IEnumerable<Rating> ratings) {
			Sketch sumSketch = new Sketch(0.1, 0.01, 99999);
			Sketch frequencySketch = new Sketch(0.1, 0.01, 99999);
			ratings.ForEach(r => {
				//Console.WriteLine(r.Item1 + " : " + r.Item2);
				sumSketch.Add(r); //sum of ratings
				frequencySketch.Add(Tuple.Create(r.Item1, 1.0)); //freq of ratings
			});
			List<double> avgs = new List<double>();
			for(int i = 1; i <= 99999; i++){
				double sum = sumSketch.Get(i);
				double freq = frequencySketch.Get(i);
				if (freq!=0.0){
					avgs.Add(sum/freq);
				}
			}
			avgs.Sort();
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
