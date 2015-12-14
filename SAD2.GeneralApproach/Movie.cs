using System;

namespace SAD2.GeneralApproach
{
	public class Movie : IComparable<Movie>
	{
		public Movie(long id, decimal rating)
		{
			Id = id;
			Rating = rating;
		}

		public long Id { get; set; }
		public long Occurences { get; set; }
		public decimal Rating { get; set; }

		public int CompareTo(Movie other)
		{
			return Rating.CompareTo(other.Rating);
		}
	}
}