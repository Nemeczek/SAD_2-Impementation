using System;
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
}
