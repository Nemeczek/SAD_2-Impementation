using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAD2.GeneralApproach
{
	public static class Extensions
	{
		public static void SaveToFile<T>(this IOrderedEnumerable<T> objectsToSave, string fileName,string path = null)  
		{	
			if(path==null)
				path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";

			using (StreamWriter outputFile = new StreamWriter($"{path}{fileName}"))
			{
				foreach (var objectToBeWritten in objectsToSave)
					outputFile.WriteLine(objectToBeWritten.ToString());
			}
		}

		private static IEnumerable<Tuple<long, decimal>> ReadAveragesFromFile(string path)
		{
			const int bufferSize = 128;
			Stack<Tuple<long, decimal>> averages = new Stack<Tuple<long, decimal>>();
			using (var fileStream = File.OpenRead(path))
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize))
			{
				string line;
				while ((line = streamReader.ReadLine()) != null)
				{
					var parts = line.Split(",".ToCharArray());
					averages.Push(new Tuple<long, decimal>(long.Parse(parts[0]), decimal.Parse(parts[1])));
				}
			}
			return averages;
		}
	}
}