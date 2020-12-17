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
      var map = File.ReadAllLines("TextFile1.txt")
        .Select((l, i) => (l, i))
        .Aggregate(new List<Tuple<int, int, int, int, char>>(), (res, line) =>
        {
          res.AddRange(line.l.Select((c, index) => new Tuple<int, int, int, int, char>(index, line.i, 0, 0, c)));
          return res;
        }).ToList();

      var map2 = map.ToDictionary(t => new Tuple<int, int, int,int>(t.Item1, t.Item2, t.Item3, t.Item4), t => t.Item5);

      for (int i = 0; i < 6; i++)
      {
        var copy = map2.ToDictionary(n => new Tuple<int, int, int, int>(n.Key.Item1, n.Key.Item2, n.Key.Item3, n.Key.Item4), n => n.Value);
        foreach (var p in map2.Keys)
          foreach (var n in GetNeighBours(map2, p))
            copy[n.Key] = n.Value;

        var copy2 = map2.ToDictionary(n => new Tuple<int, int, int, int>(n.Key.Item1, n.Key.Item2, n.Key.Item3, n.Key.Item4), n => n.Value);
        foreach (var p in map2.Keys)
          foreach (var n in GetNeighBours(map2, p))
            copy2[n.Key] = n.Value;
        foreach (var p in copy2)
        {
          var countActive = GetNeighBours(map2, p.Key).Count(n => n.Value == '#');
          if (p.Value == '#' && countActive != 2 && countActive != 3)
            copy[p.Key] = '.';

          if (p.Value == '.' && countActive == 3)
            copy[p.Key] = '#';

        map2 = copy;

        if(i == 5)
          Console.WriteLine(copy.Values.Count(v => v == '#'));
      }
    }

    static List<KeyValuePair<Tuple<int, int, int, int>, char>> GetNeighBours(Dictionary<Tuple<int, int, int, int>, char> map, Tuple<int, int, int, int> aPoint)
    {
      var res = new List<KeyValuePair<Tuple<int, int, int, int>, char>>();
      for (int i = -1; i <= 1; i++)
        for (int j = -1; j <= 1; j++)
          for (int k = -1; k <= 1; k++)
            for (int l = -1; l <= 1; l++)
            {
            var n = new Tuple<int, int, int, int>(aPoint.Item1 + i, aPoint.Item2 + j, aPoint.Item3 + k, aPoint.Item4 + l);
            if (n.Item1 == aPoint.Item1 && n.Item2 == aPoint.Item2 && n.Item3 == aPoint.Item3 && n.Item4 == aPoint.Item4)
              continue;

            char c = '.';
            if (map.ContainsKey(n))
              c = map[n];

            res.Add(new KeyValuePair<Tuple<int, int, int, int>, char>(n, c));
          }
      return res;
    }
  }
}

