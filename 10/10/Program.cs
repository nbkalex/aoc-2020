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

      var res = numbers.Skip(1).Zip(numbers).Select(n => n.First - n.Second)                                        // diffs                                                                           // as string            
        .Aggregate(numbers.First().ToString(), (acc, n) => acc + n.ToString())                                      // as string
        .Split('3')                                                                                                 // group of 1
        .Where(g => g.Length > 1)                                                                                   // exclude irelevand groups
        .Aggregate((long)1, (acc, g) => g.Length == 2 ? acc * 2 :  acc * (4 * (g.Length - 2) - (g.Length - 3)));    // compute value

      Console.WriteLine(res);
    }
  }
}
