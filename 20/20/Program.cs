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
      Dictionary<int, List<string>> tiles = new Dictionary<int, List<string>>();
      int currentTile = -1;
      foreach(var line in File.ReadAllLines("TextFile1.txt"))
      {
        if (line.StartsWith("Tile "))
        {
          currentTile = int.Parse(new string(line.Substring("Tile ".Length).SkipLast(1).ToArray()));
          tiles.Add(currentTile, new List<string>());
        }
        else if (line == "")
          continue;
        else
          tiles[currentTile].Add(line);

      }


      Dictionary<int, List<string>> tilesEdges = tiles.ToDictionary(tile => tile.Key, tile =>
      new List<string>()
      {
        tile.Value.First(),
        new string(tile.Value.First().Reverse().ToArray()),
        tile.Value.Last(),
        new string(tile.Value.Last().Reverse().ToArray()),
        new string(tile.Value.Select(t => t.First()).ToArray()),
        new string(tile.Value.Select(t => t.First()).Reverse().ToArray()),
        new string(tile.Value.Select(t => t.Last()).ToArray()),
        new string(tile.Value.Select(t => t.Last()).Reverse().ToArray())
      });

      var neighbours = tilesEdges.ToDictionary(te => te.Key,
        te => tilesEdges.Where(te2 => te2.Key != te.Key && te2.Value.Intersect(te.Value).Any()).ToList());

      Console.WriteLine(neighbours.Where(n => n.Value.Count == 3).Aggregate((long)1, (acc, n) => acc * n.Key));
    }

   

    static List<string> FlipVertical(List<string> tile)
    {
      var res = tile.ToList();
      res.Reverse();
      return res;
    }

    static List<string> FlipHorizontal(List<string> tile)
    {
      return tile.Select(t => new string(t.Reverse().ToArray())).ToList();
    }

    static List<string> RotateLeft(List<string> tile)
    {
      List<string> res = new List<string>();
      for (int i = 0; i < tile.Count; i++)
        res.Add(new string(tile.Select(t => t[tile.Count - i - 1]).ToArray()));

      return res;
    }

    static List<string> RotateRight(List<string> tile)
    {
      List<string> res = new List<string>();
      for (int i = 0; i < tile.Count; i++)
        res.Add(new string(tile.ToArray().Reverse().Select(t => t[i]).ToArray())); ;

      return res;
    }
  }
}
