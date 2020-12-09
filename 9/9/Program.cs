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
      var numbers = File.ReadAllLines("TextFile1.txt").Select(line => long.Parse(line)).ToList();

      var invalid = numbers.Select((number, index) => new { number, index })
        .ToList()
        .GetRange(rangeSize, numbers.Count - rangeSize - 1)
        .FirstOrDefault(num => !numbers.GetRange((int)num.index - rangeSize, rangeSize)
          .Any(n => numbers.GetRange((int)num.index - rangeSize, rangeSize).
            Any(rest => rest + n == num.number)));

      foreach (var size in Enumerable.Range(2, rangeSize))
      {
        foreach (var index in Enumerable.Range(0, numbers.Count - rangeSize))
        {
          var r = numbers.GetRange(index, size);
          if (r.Sum() == invalid.number)
            Console.WriteLine(r.Min() + r.Max());
        }
      }
    }
  }
}
