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
      var data = File.ReadAllLines("TextFile1.txt");
      var time = int.Parse(data[0]);
      var res = data[1].Split(',').Where(b => b != "x").Select(b => int.Parse(b)).Select(b => (b, b - time % b));
      int min = res.Min(b => b.Item2);
      int first = res.FirstOrDefault(b => b.Item2 == min).b * min;

      var busses = data[1].Split(',')
        .Select((b, i) => (b, i))
        .Where(b => b.b != "x")
        .Select(b => (int.Parse(b.b), b.i));

      long lcm = 1;
      long found = 0;
      foreach(var b in busses)
      {
        while (found == 0 || (found + b.i) % b.Item1 != 0)
          found += lcm;

        lcm = determineLCM(lcm, b.Item1);
      }

    }

    public static long determineLCM(long a, long b)
    {
      long num1, num2;
      if (a > b)
      {
        num1 = a; num2 = b;
      }
      else
      {
        num1 = b; num2 = a;
      }

      for (int i = 1; i < num2; i++)
      {
        long mult = num1 * i;
        if (mult % num2 == 0)
        {
          return mult;
        }
      }
      return num1 * num2;
    }

  }
}
