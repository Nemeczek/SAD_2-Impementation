using System;
using System.Collections.Generic;

namespace SAD2.KendallTauDistance
{
	public static class KendallTau
	{
		public static decimal Distance(decimal[] a, decimal[] b)
		{
			if (a.Length != b.Length)
				throw new ArgumentException("Array dimensions disagree");

			long n = a.Length;
			Dictionary<decimal, int> ainvD = new Dictionary<decimal, int>();

			for (int i = 0; i < n; i++)
			{
				if(!ainvD.ContainsKey(a[i]))
				ainvD.Add(a[i], i);
			}
				

			int[] bnew = new int[n];
			for (int i = 0; i < n; i++)
			{
				if(ainvD.ContainsKey(b[i]))
					bnew[i] = ainvD[b[i]];
			}

			var inversions = Inversions.count(bnew);
			decimal bottom = (n*(n - 2))/2.0M;
			
			return Math.Round(inversions / bottom, 1, MidpointRounding.ToEven); ;
		}
	}
}
