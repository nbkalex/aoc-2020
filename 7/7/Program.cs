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
    static Dictionary<string, List<Tuple<int, string>>> constraints = new Dictionary<string, List<Tuple<int, string>>>();

    static bool Contains(string bag)
    {
      return constraints[bag].Any(b => b.Item2 == "shiny gold" || Contains(b.Item2));
    }

    static int Sum(string bag)
    {      
      return constraints[bag].Sum(b => b.Item1 * (1+ Sum(b.Item2)));
    }

    static void Main(string[] args)
    {
      string[] content = File.ReadAllLines("TextFile1.txt");
      constraints = content.Select(l =>
      {
        var bagType = l.Split(" bags contain ");
        return new KeyValuePair<string, List<Tuple<int, string>>>(bagType[0], 
        bagType[1].Split(", ").Where(b => b.Trim().Split(" ").Length == 4).Select(b =>
        {
          var bags = b.Split(" ");
          return new Tuple<int, string>(int.Parse(bags[0]), string.Join(" ", bags.Skip(1).SkipLast(1)));
        }).ToList());
      }).ToDictionary(p => p.Key, p => p.Value);


      Console.WriteLine(constraints.Count(c => Contains(c.Key)));
      Console.WriteLine(Sum("shiny gold"));
    }
  }
}
