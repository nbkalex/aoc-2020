using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day2
{
  struct password
  {
    public int min, max;
    public char c;
    public string val;
  }
  class Program
  {
    static void Main(string[] args)
    {
      int count = 0;
      List<password> passwords = new List<password>();
      using (StreamReader sr = new StreamReader("TextFile1.txt"))
      {
        for (string line = line = sr.ReadLine(); line != null; line = sr.ReadLine())
        {
          var metaVal = line.Split(":");
          var intervalChar = metaVal[0].Split(" ");
          string[] minMax = intervalChar[0].Split("-");

          char c = intervalChar[1][0];          
          int min = int.Parse(minMax[0]);
          int max = int.Parse(minMax[1]);
          string val = metaVal[1].Trim();
          passwords.Add(new password { min = min, max = max, c= c, val = val });

          // part 1
          //int countC = val.Count(z => z == c);
          //if (countC >= min && countC <= max)
          //  count++;

          // part2
          //if ((val[min-1] == c) ^ (val[max-1] == c))
          // count++;

        }
      }

      Console.WriteLine(passwords.Count(p => (p.val[p.min - 1] == p.c) ^ (p.val[p.max - 1] == p.c)));
      Console.WriteLine(passwords.Count(p => { int count = p.val.Count(c => c == p.c); return count >= p.min && count <= p.max; }));

      Console.WriteLine(count);
    } 
  }
}
