using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day2
{
  class Program
  {

    const int rangeSize = 25;

    static void Main(string[] args)
    {
      var numbers = File.ReadAllLines("TextFile1.txt")
        .Select(line => int.Parse(line))
        .OrderBy(n => n)
        .ToList();

      var init = new int[] { 1, 2, 4 };
      var perms = new int[10];
      perms = perms.Select((p, i) => perms[i] = i < 3 ? init[i] : perms[i - 1] + perms[i - 2] + perms[i - 3]).ToArray();

      Console.WriteLine(
        numbers.Skip(1)
               .Zip(numbers).Select(n => n.First - n.Second)                                        // diffs                 
               .Aggregate(numbers.First().ToString(), (acc, n) => acc + n.ToString())               // as string
               .Split('3', StringSplitOptions.RemoveEmptyEntries)                                   // group of 1
               .Aggregate((long)1, (acc, g) => acc * perms[g.Length-1]));                           // compute value 
    }
  }
}
