namespace SAD2.GeneralApproach
{
	public class Movie
	{
		public Movie(long id, decimal rating)
		{
			Id = id;
			Rating = rating;
		}

		public long Id { get; set; }
		public long Occurences { get; set; }
		public decimal Rating { get; set; }

		public override string ToString()
		{
			return $"{Id},{Rating/Occurences}";
		}
	}
}
