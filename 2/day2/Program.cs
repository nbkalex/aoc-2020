using System;
using System.IO;

namespace day2
{
  class Program
  {
    static void Main(string[] args)
    {
      int count = 0;
      using (StreamReader sr = new StreamReader("TextFile1.txt"))
      {
        for(string line = line = sr.ReadLine(); line != null; line = sr.ReadLine())
        {
          var metaVal = line.Split(":");
          var intervalChar = metaVal[0].Split(" ");
          string[] minMax = intervalChar[0].Split("-");

          char c = intervalChar[1][0];          
          int min = int.Parse(minMax[0]);
          int max = int.Parse(minMax[1]);
          string val = metaVal[1].Trim();

          // part 1
          //int countC = 0;
          //foreach(var v in val)
          //  if (v == c)
          //    countC++;

          //if (countC >= min && countC <= max)
          //  count++;

          // part2
           if ((val[min-1] == c) ^ (val[max-1] == c))
            count++;

        }
      }

      Console.WriteLine(count);
    } 
  }
}
