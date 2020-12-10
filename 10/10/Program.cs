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
      numbers.Insert(0, 0);
      numbers.Add(numbers.Last() + 3);
      List<List<long>> groups = new List<List<long>>() { new List<long>() };
      for (int i = 0; i < numbers.Count-1; i++)
      {
        groups.Last().Add(numbers[i]);

        if (numbers[i + 1] - numbers[i] != 1)
          groups.Add(new List<long>());
      }

      long res = 1;
      foreach (var g in groups)
      {
        if (g.Count == 3)
          res *= 2;
        if (g.Count == 4)
          res *= 4;

        if (g.Count > 4)
          res *= 4 * (g.Count - 4 + 1) - (g.Count - 4);
      }

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
