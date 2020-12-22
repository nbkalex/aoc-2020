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
    static void Main(string[] args)
    {
      var recipes = File.ReadAllLines("TextFile1.txt").ToDictionary(line => line.Split(" (contains ")[0].Split(" ").ToList(), line => line.Replace(")", "").Split(" (contains ")[1].Split(", ").ToList());
      var ingredients = recipes.Aggregate(new List<string>(), (acc, r) => { acc.AddRange(r.Key); return acc; }).Distinct().ToList();
      var aleg = recipes.Aggregate(new List<string>(), (acc, r) => { acc.AddRange(r.Value); return acc; }).Distinct().ToList();
      Dictionary<string, string> alegFound = new Dictionary<string, string>();
      while (alegFound.Count != aleg.Count)
      {
        foreach (var a in aleg)
        {
          var rec = recipes.Where(r => r.Value.Contains(a)).ToList();
          if (rec.Count == 0)
            continue;

          IEnumerable<string> intersec = rec.First().Key;
          foreach (var r in rec)
            intersec = intersec.Intersect(r.Key);

          if (intersec.Count() == 1)
          {
            string ingredient = intersec.First();
            alegFound.Add(a, ingredient);
            foreach (var r in recipes)
            {
              r.Key.Remove(ingredient);
              r.Value.Remove(a);
            }
          }
        }
      }

      var val = recipes.Aggregate(0, (acc, r) => acc + r.Key.Count(i => !alegFound.Values.Contains(i)));
      var val2 = string.Join(",", alegFound.OrderBy(a => a.Key).Select(a => a.Value));
    }
  }
}
