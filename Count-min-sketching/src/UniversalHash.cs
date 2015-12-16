using System;
using Rating = System.Tuple<long,double>;
using Hash = System.Func<long,int>;
using SketchVector = System.Tuple<System.Func<long,int>, double[]>;

class UniversalHash {
	static private Random r = new Random();
	long a, b, n, q;
	const long p = 100003;

	public UniversalHash(long domainSize, long imageSize){
		this.n = domainSize;
		this.q = imageSize;

		if(p <= n || imageSize >= (2^31)) {
			throw new ArgumentException("The (stupidly) hardcoded prime is no larger than (n = " + n + "). Find a new prime and recompile.." ); 
		}

		a = RandomLong(1, p);
		b = RandomLong(0, p+1);
	}

	public int Hash(long x) {
		if(n < x) {
			throw new ArgumentException("Trying to hash a value larger than the domain maximum" ); 
		}

		return (int)(((a*x+b) % p) % q);
	}

	// l <= RandomLong(f, t) < t
	private long RandomLong(long from, long to) {
		double rand = r.NextDouble();
		return from + (long)(rand*(to-from));
	}
}

class Sketch{
	int d; //number of sketches
	SketchVector[] sketches; //our family of hash functions

	public Sketch(double epsilon, double delta, long domainSize){
		// imageSize corresponds to w in ikonomovska
		int imageSize = (int) Math.Ceiling(2/epsilon);
		d = (int) Math.Ceiling(Math.Log(1/delta));
		sketches = new Tuple<Hash, double[]>[d];

		for (int i = 0; i<d; i++){

			UniversalHash h = new UniversalHash(domainSize, imageSize);
			sketches[i] = Tuple.Create<Hash, double[]>(h.Hash, new double[imageSize]);
		}
	}

	public void Add(Rating rating){
		foreach (SketchVector sv in sketches){
			Hash h = sv.Hash();
			int hashvalue = h(rating.Item1);
			double[] counters = sv.Counters();

			counters[hashvalue] += rating.Item2;
		}
	}

	public double Get(long movie){
		double minCount=Double.MaxValue;
		foreach (SketchVector sv in sketches) {
			double count = sv.Counters()[sv.Hash()(movie)];
			minCount = Math.Min(count, minCount);
		}

		return minCount;
	}
}

static class Extensions{
	public static Hash Hash(this SketchVector sketch){
		return sketch.Item1;
	}

	public static double[] Counters(this SketchVector sketch){
		return sketch.Item2;
	}
}
