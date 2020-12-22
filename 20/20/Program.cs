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

      Dictionary<int, int> rotation = tiles.ToDictionary(t => t.Key, t => 0);
      Dictionary<int, HashSet<int>[]> neighbours = tiles.ToDictionary(t => t.Key, t => new HashSet<int>[]{ new HashSet<int>(),new HashSet<int>(),new HashSet<int>(),new HashSet<int>() }); // N S E V

      foreach (var tile in tiles)
      {
        foreach (var other in tiles)
        {
          if (tile.Key == other.Key)
            continue;

          int match = GetMatch(tile.Value, other.Value);
          if (match != -1)
            neighbours[tile.Key][match].Add(other.Key);

          match = GetMatch(tile.Value, RotateLeft(other.Value));
          if (match != -1)
            neighbours[tile.Key][match].Add(other.Key);
        }
      }

      Console.WriteLine(string.Join(" \n",neighbours.Select(n => n.Key.ToString() + "-"+ n.Value.Count(v => v.Count != 0).ToString()).ToArray()));
      Console.WriteLine(neighbours.Where(n => n.Value.Count(v => v.Count == 0) == 2).Aggregate((long)1, (acc, v) => acc * v.Key));
    }

    static int GetMatch(List<string> t1, List<string> tile2)
    {
      //var tile1 = RotateRight(FlipHorizontal(t1));
      var tile1 = t1;

      if (tile1.First() == tile2.First() || tile1[0] == tile2.Last())
        return 0;

      if (tile1.First() == new string(tile2.First().Reverse().ToArray()) || tile1[0] == new string(tile2.Last().Reverse().ToArray())) //flip
        return 0;

      if (tile1.Last() == tile2.First() || tile1.Last() == tile2.Last())
        return 1;

      if (tile1.Last() == new string(tile2.First().Reverse().ToArray()) || tile1.Last() == new string(tile2.Last().Reverse().ToArray())) //flip
        return 1;

      var zip = tile1.Zip(tile2);
      if (zip.All(z => z.First.First() == z.Second.First() || z.First.First() == z.Second.Last()))
        return 2;

      if (zip.All(z => z.First.Last() == z.Second.First() || z.First.Last() == z.Second.Last()))
        return 3;

      var zipFlip = tile1.Zip(tile2.ToArray().Reverse());
      if (zipFlip.All(z => z.First.First() == z.Second.First() || z.First.First() == z.Second.Last()))
        return 2;

      if (zipFlip.All(z => z.First.Last() == z.Second.First() || z.First.Last() == z.Second.Last()))
        return 3;

      tile2 = RotateLeft(tile2);

      if (tile1.First() == tile2.First() || tile1[0] == tile2.Last())
        return 0;

      if (tile1.First() == new string(tile2.First().Reverse().ToArray()) || tile1[0] == new string(tile2.Last().Reverse().ToArray())) //flip
        return 0;

      if (tile1.Last() == tile2.First() || tile1.Last() == tile2.Last())
        return 1;

      if (tile1.Last() == new string(tile2.First().Reverse().ToArray()) || tile1.Last() == new string(tile2.Last().Reverse().ToArray())) //flip
        return 1;

      zip = tile1.Zip(tile2);
      if (zip.All(z => z.First.First() == z.Second.First() || z.First.First() == z.Second.Last()))
        return 2;

      if (zip.All(z => z.First.Last() == z.Second.First() || z.First.Last() == z.Second.Last()))
        return 3;

      zipFlip = tile1.Zip(tile2.ToArray().Reverse());
      if (zipFlip.All(z => z.First.First() == z.Second.First() || z.First.First() == z.Second.Last()))
        return 2;

      if (zipFlip.All(z => z.First.Last() == z.Second.First() || z.First.Last() == z.Second.Last()))
        return 3;

      return -1;
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
