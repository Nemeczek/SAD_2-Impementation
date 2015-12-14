using System;

namespace SAD2.KendallTauDistance
{
	public class Inversions
	{
		// do not instantiate
		private Inversions() { }

		// merge and count
		private static long merge(int[] a, int[] aux, int lo, int mid, int hi)
		{
			long inversions = 0;

			// copy to aux[]
			for (int k = lo; k <= hi; k++)
			{
				aux[k] = a[k];
			}

			// merge back to a[]
			int i = lo, j = mid + 1;
			for (int k = lo; k <= hi; k++)
			{
				if (i > mid) a[k] = aux[j++];
				else if (j > hi) a[k] = aux[i++];
				else if (aux[j] < aux[i]) { a[k] = aux[j++]; inversions += (mid - i + 1); }
				else a[k] = aux[i++];
			}
			return inversions;
		}

		// return the number of inversions in the subarray b[lo..hi]
		// side effect b[lo..hi] is rearranged in ascending order
		private static long count(int[] a, int[] b, int[] aux, int lo, int hi)
		{
			long inversions = 0;
			if (hi <= lo) return 0;
			int mid = lo + (hi - lo) / 2;
			inversions += count(a, b, aux, lo, mid);
			inversions += count(a, b, aux, mid + 1, hi);
			inversions += merge(b, aux, lo, mid, hi);
			return inversions;
		}


		/**
		 * Returns the number of inversions in the integer array.
		 * The argument array is not modified.
		 * @param  a the array
		 * @return the number of inversions in the array. An inversion is a pair of 
		 *         indicies <tt>i</tt> and <tt>j</tt> such that <tt>i &lt; j</tt>
		 *         and <tt>a[i]</tt> &gt; <tt>a[j]</tt>.
		 */
		public static long count(int[] a)
		{
			int[] b = new int[a.Length];
			int[] aux = new int[a.Length];
			for (int i = 0; i < a.Length; i++)
				b[i] = a[i];
			long inversions = count(a, b, aux, 0, a.Length - 1);
			return inversions;
		}



		// merge and count (IComparable version)
		private static long merge(IComparable[] a, IComparable[] aux, int lo, int mid, int hi)
		{
			long inversions = 0;

			// copy to aux[]
			for (int k = lo; k <= hi; k++)
			{
				aux[k] = a[k];
			}

			// merge back to a[]
			int i = lo, j = mid + 1;
			for (int k = lo; k <= hi; k++)
			{
				if (i > mid) a[k] = aux[j++];
				else if (j > hi) a[k] = aux[i++];
				else if (less(aux[j], aux[i])) { a[k] = aux[j++]; inversions += (mid - i + 1); }
				else a[k] = aux[i++];
			}
			return inversions;
		}

		// return the number of inversions in the subarray b[lo..hi]
		// side effect b[lo..hi] is rearranged in ascending order
		private static long count(IComparable[] a, IComparable[] b, IComparable[] aux, int lo, int hi)
		{
			long inversions = 0;
			if (hi <= lo) return 0;
			int mid = lo + (hi - lo) / 2;
			inversions += count(a, b, aux, lo, mid);
			inversions += count(a, b, aux, mid + 1, hi);
			inversions += merge(b, aux, lo, mid, hi);
			return inversions;
		}


		/**
		 * Returns the number of inversions in the IComparable array.
		 * The argument array is not modified.
		 * @param  a the array
		 * @return the number of inversions in the array. An inversion is a pair of 
		 *         indicies <tt>i</tt> and <tt>j</tt> such that <tt>i &lt; j</tt>
		 *         and <tt>a[i].compareTo(a[j]) &gt; 0</tt>.
		 */
		public static long count(IComparable[] a)
		{
			IComparable[] b = new IComparable[a.Length];
			IComparable[] aux = new IComparable[a.Length];
			for (int i = 0; i < a.Length; i++)
				b[i] = a[i];
			long inversions = count(a, b, aux, 0, a.Length - 1);
			return inversions;
		}


		// is v < w ?
		private static bool less(IComparable v, IComparable w)
		{
			return (v.CompareTo(w) < 0);
		}

		// count number of inversions in a[lo..hi] via brute force (for debugging only)
		private static long brute(IComparable[] a, int lo, int hi)
		{
			long inversions = 0;
			for (int i = lo; i <= hi; i++)
				for (int j = i + 1; j <= hi; j++)
					if (less(a[j], a[i])) inversions++;
			return inversions;
		}

		// count number of inversions in a[lo..hi] via brute force (for debugging only)
		private static long brute(int[] a, int lo, int hi)
		{
			long inversions = 0;
			for (int i = lo; i <= hi; i++)
				for (int j = i + 1; j <= hi; j++)
					if (a[j] < a[i]) inversions++;
			return inversions;
		}
	}
}