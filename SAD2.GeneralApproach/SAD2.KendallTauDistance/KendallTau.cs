using System;
using System.Collections.Generic;

namespace SAD2.KendallTauDistance
{
	public static class KendallTau
	{
		public static long Distance(decimal[] a, decimal[] b)
		{
			if (a.Length != b.Length)
				throw new ArgumentException("Array dimensions disagree");

			int N = a.Length;

			int[] ainv = new int[N];
			Dictionary<decimal, int> ainvD = new Dictionary<decimal, int>();

			for (int i = 0; i < N; i++)
			{
				if(!ainvD.ContainsKey(a[i]))
				ainvD.Add(a[i], i);
			}
				

			int[] bnew = new int[N];
			Dictionary<decimal, int> bnewD = new Dictionary<decimal, int>();
			for (int i = 0; i < N; i++)
			{
				if(ainvD.ContainsKey(b[i]))
					bnew[i] = ainvD[b[i]];
			}
				

			return Inversions.count(bnew);
		}
	}
}
