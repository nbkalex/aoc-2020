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
      var input = File.ReadAllLines("TextFile1.txt");
      List<Tuple<string, List<int>>> info = new List<Tuple<string, List<int>>>();
      int i = 0;
      while (input[i] != "")
      {
        string[] infoLine = input[i].Split(": ");
        string[] ranges = infoLine[1].Split(" or ");
        var firstRange = (int.Parse(ranges[0].Split("-")[0]), int.Parse(ranges[0].Split("-")[1]) + 1);
        var secondRange = (int.Parse(ranges[1].Split("-")[0]), int.Parse(ranges[1].Split("-")[1]) + 1);
        var list = new List<int>(Enumerable.Range(firstRange.Item1, firstRange.Item2 - firstRange.Item1));
        list.AddRange(Enumerable.Range(secondRange.Item1, secondRange.Item2 - secondRange.Item1));
        info.Add(new Tuple<string, List<int>>(infoLine[0], list));

        i++;
      }


      // read your ticket
      i += 2;
      var yourTicket = input[i].Split(",").Select(n => int.Parse(n)).ToList();

      // read other tickets
      List<List<int>> otherTickets = new List<List<int>>();
      for (i = i + 3; i < input.Length; i++)
        otherTickets.Add(input[i].Split(",").Select(n => int.Parse(n)).ToList());

      var allRanges = info.Aggregate(new List<List<int>>(), (l, r) => { l.Add(r.Item2); return l; });
      Console.WriteLine(otherTickets.Sum(t => t.Where(val => !allRanges.Any(r => r.Contains(val))).Sum()));
      var validTickets = otherTickets.Where(t => t.All(val => allRanges.Any(r => r.Contains(val)))).ToList();
      int valuesCount = validTickets.First().Count();
      var mapped2 = info.ToDictionary(
        row => row,
        row => Enumerable.Range(0, valuesCount).Where(index => validTickets.All(t => row.Item2.Contains(t[index]))).ToList());

      var ordered = mapped2.OrderBy(m => m.Value.Count());

      List<int> found = new List<int>();
      Dictionary<Tuple<string, List<int>>, int> mapped = new Dictionary<Tuple<string, List<int>>, int>();
      foreach(var m in ordered)
      {
        found.Add(m.Value.FirstOrDefault(index => !found.Contains(index)));
        mapped[m.Key] = found.Last();
        foreach (var m2 in mapped2)
          m2.Value.Remove(found.Last());

      }

      Console.WriteLine(info.Where(range => range.Item1.StartsWith("departure")).Aggregate((long)1, (r, range) => r * yourTicket[mapped[range]]));
    }
  }
}
