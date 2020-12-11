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
        .Select(line => long.Parse(line))
        .OrderBy(n => n)
        .ToList();

      var res = numbers.Select((n, i) => i < numbers.Count - 1 ? numbers[i + 1] - n : 3)
        .Select((n, i) => new { i, n })
        .Aggregate(numbers.First().ToString(), (acc, n) => acc + n.n.ToString())
        .Split('3')
        .Where(g => g.Length > 1)
        .Aggregate((long)1, (acc, g) => g.Length == 2 ? acc * 2 :  acc * (4 * (g.Length - 2) - (g.Length - 3)));

      Console.WriteLine(res);
    }

    static long Fact(int nr)
    {
      long res = 1;
      foreach (var n in Enumerable.Range(2, nr))
        res *= n;

      return res;
    }
  }
}
