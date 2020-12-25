using System;
using System.Collections.Generic;
using System.Drawing;
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

      var input = File.ReadAllLines("TextFile1.txt");

      Dictionary<Point, bool> tiles = new Dictionary<Point, bool>();

      Dictionary<char, Point> dirs = new Dictionary<char, Point>()
      {
        { 'e', new Point(2,0)},
        { 'w', new Point(-2,0)},
        { '1', new Point(1,-1)},
        { '2', new Point(-1,1)},
        { '3', new Point(-1,-1)},
        { '4', new Point(1,1)},
      };

      input.Select(line =>
      {
      var pos = line.Replace("se", "1").Replace("sw", "3").Replace("nw", "2").Replace("ne", "4")
      .Aggregate(new Point(0, 0), (acc, dir) => { acc.X += dirs[dir].X; acc.Y += dirs[dir].Y; return acc; });
        if (tiles.ContainsKey(pos))
          tiles[pos] = !tiles[pos];
        else
          tiles[pos] = false;
        return 0;
        }).ToList();

      Console.WriteLine(tiles.Count(t => t.Value == false));

      for (int i = 0; i < 100; i++)
      {
        var currentTiles = tiles.Keys.ToList();

        // add neighbours
        foreach (var t in currentTiles)
          foreach (var dir in dirs)
            if (!tiles.ContainsKey(new Point(t.X + dir.Value.X, t.Y + dir.Value.Y)))
              tiles.Add(new Point(t.X + dir.Value.X, t.Y + dir.Value.Y), true);

        var copy = tiles.ToDictionary(t => t.Key, t => t.Value);
        var withBlackNeighb = copy.Keys.Select(t => (t, (dirs.Count(d => copy.ContainsKey(new Point(t.X + d.Value.X, t.Y + d.Value.Y)) && !tiles[new Point(t.X + d.Value.X, t.Y + d.Value.Y)])))).ToList();
        foreach (var wbn in withBlackNeighb)
        {
          if (copy[wbn.t] && wbn.Item2 == 2)
            tiles[wbn.t] = false;
          else if (!copy[wbn.t] && (wbn.Item2 == 0 || wbn.Item2 > 2))
            tiles[wbn.t] = true;
        }

        Console.WriteLine(tiles.Count(t => t.Value == false));
      }

      Console.WriteLine(tiles.Count(t => t.Value == false));
    }
  }
}
