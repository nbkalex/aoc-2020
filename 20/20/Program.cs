using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace day2
{
  class Program
  {
    static void Main(string[] args)
    {
      Dictionary<int, List<string>> tiles = new Dictionary<int, List<string>>();
      int currentTile = -1;
      foreach (var line in File.ReadAllLines("TextFile1.txt"))
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
        tile.Value.Last(),
        new string(tile.Value.Select(t => t.First()).ToArray()),
        new string(tile.Value.Select(t => t.Last()).ToArray()),
        new string(tile.Value.First().Reverse().ToArray()),                // flipped horozontally
        new string(tile.Value.Last().Reverse().ToArray()),                 // flipped horozontally
        new string(tile.Value.Select(t => t.First()).Reverse().ToArray()), // flipped vertically
        new string(tile.Value.Select(t => t.Last()).Reverse().ToArray())   // flipped vertically
      });

      var neighbours = tilesEdges.ToDictionary(te => te.Key,
        te => tilesEdges.Where(te2 => te2.Key != te.Key && te2.Value.Intersect(te.Value).Any()).Select(t => t.Key).ToList());

      Stack<int> toVisit = new Stack<int>();
      HashSet<int> visited = new HashSet<int>();
      toVisit.Push(neighbours.FirstOrDefault(n => n.Value.Count == 2).Key);

      Dictionary<int, Point> drawing = new Dictionary<int, Point>() { { toVisit.First(), new Point(0, 0) } };
      //PrintTile(tiles[toVisit.First()], drawing.First().Value);
      while (toVisit.Count > 0)
      {
        int current = toVisit.Pop();
        visited.Add(current);

        var tile = tiles[current];
        var edges = GetEdges(tile);
        foreach (var edge in edges)
        {
          var nFound = neighbours[current].FirstOrDefault(n => tilesEdges[n].Contains(edge));
          if (nFound == 0 || visited.Contains(nFound) || drawing.ContainsKey(nFound))
            continue;

          tiles[nFound] = GetNeighbourTile(tiles[nFound], edge, edges.IndexOf(edge));
          Point draw = new Point(drawing[current].X, drawing[current].Y);
          if (edges.IndexOf(edge) == 0)
            draw.Y -= tiles[nFound].Count;
          if (edges.IndexOf(edge) == 1)
            draw.Y += tiles[nFound].Count;
          if (edges.IndexOf(edge) == 2)
            draw.X -= tiles[nFound].First().Length;
          if (edges.IndexOf(edge) == 3)
            draw.X += tiles[nFound].First().Length;

          drawing.Add(nFound, draw);
          //PrintTile(tiles[nFound], draw);
        }

        foreach (var n in neighbours[current])
          if (!visited.Contains(n))
            toVisit.Push(n);
      }

      var minx = drawing.Values.Min(p => p.X);
      var miny = drawing.Values.Min(p => p.Y);
      var maxx = drawing.Values.Max(p => p.X);
      var maxy = drawing.Values.Max(p => p.Y);

      Console.WriteLine();

      var map = drawing.OrderBy(d => d.Value.Y).ThenBy(d => d.Value.X).Select(d => d.Key).ToList();
      foreach (var t in map)
      {
        if (map.IndexOf(t) % 12 == 0)
          Console.WriteLine();

        Console.Write(t + " ");
      }

      Console.WriteLine();
      Console.WriteLine();
      char[,] image = new char[maxx - minx + tiles.First().Value.First().Length, maxy - miny + tiles.First().Value.Count];
      foreach (var tile in tiles)
      {
        var start = drawing[tile.Key];
        start.X -= minx;
        start.Y -= miny;
        for (int i = 0; i < tile.Value.Count; i++)
        {
          for (int j = 0; j < tile.Value.First().Length; j++)
            image[start.Y + i, start.X + j] = tile.Value[i][j];
        }
      }

      var imgFormatted = new List<string>();
      for (int i = 0; i < maxx - minx + tiles.First().Value.First().Length; i++)
      {
        string line = "";
        for (int j = 0; j < maxy - miny + tiles.First().Value.Count; j++)
          line += image[i, j];
        imgFormatted.Add(line);
      }

      var imgFormatted2 = new List<string>();
      for (int i = 0; i < imgFormatted.Count; i++)
      {
        string l = imgFormatted[i];

        if (i % tiles.First().Value.Count == 0 || i % tiles.First().Value.Count == tiles.First().Value.Count - 1)
          continue;

        string fmt = "";
        for (int j = 0; j < l.Length; j++)
        {
          if (j % tiles.First().Value.First().Length == 0 || j % tiles.First().Value.First().Length == tiles.First().Value.First().Length - 1)
            continue;

          fmt += imgFormatted[i][j];
        }

        imgFormatted2.Add(fmt);
      }

      PrintTile(FlipVertical(imgFormatted2));

      foreach (var op in Operations)
      {

        List<string> imgCopy = imgFormatted2.ToList();
        
        if (op != null)
          imgCopy = op(imgCopy);

        foreach (var op2 in Operations)
        {
          if (op != null)
            imgCopy = op(imgCopy);

          foreach (var op3 in Operations)
          {
            if (op != null)
              imgCopy = op(imgCopy);

            bool monsterFound = false;
            for (int i = 0; i < imgCopy.Count; i++)
            {
              for (int j = 0; j < imgCopy.First().Length; j++)
              {
                if (HasMonster(imgCopy, i, j))
                {
                  SetMonster(imgCopy, i, j);
                  monsterFound = true;
                }
              }
            }

            if(monsterFound)
            {
              PrintTile(FlipVertical(imgCopy));
              Console.WriteLine(imgCopy.Sum(l => l.Count(c => c == '#')));
              return;
            }
          }
        }
      }


      //  Console.WriteLine(neighbours.Where(n => n.Value.Count == 2).Aggregate((long)1, (acc, n) => acc * n.Key));
    }

    static Dictionary<int, int> EdgeMatch = new Dictionary<int, int>()
    {
      {0,1 },
      {1,0 },
      {2,3 },
      {3,2 }
    };

    static List<Func<List<string>, List<string>>> Operations = new List<Func<List<string>, List<string>>>()
      {
        null, null, null, FlipVertical, FlipHorizontal, (e) => Rotate(e, 1),  (e) => Rotate(e, 2),   (e) => Rotate(e, 3)
      };

    static List<string> GetNeighbourTile(List<string> neighbour, string edge, int index)
    {
      var res = neighbour.ToList();

      foreach ( var op  in Operations)
      {
        if (op != null)
          res = op(res);

        foreach (var op2 in Operations)
        {
          if (op2 != null)
            res = op2(res);

          foreach (var op3 in Operations)
          {
            if (op3 != null)
              res = op3(res);

            var edges = GetEdges(res);
            if (edges.Contains(edge) && EdgeMatch[index] == edges.IndexOf(edge))
              return res;
          }
        }
      }

      throw new Exception("not found");
    }

    static List<string> GetEdges(List<string> tile)
    {
      return new List<string>()
      {
        tile.First(),
        tile.Last(),
        new string(tile.Select(t => t.First()).ToArray()),
        new string(tile.Select(t => t.Last()).ToArray())
      };
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

    static List<string> Rotate(List<string> tile, int times)
    {
      List<string> res = tile.ToList();
      for (int i = 0; i < times; i++)
        res = RotateLeft(res);
      return res;
    }

    static void PrintTile(List<string> tile)
    {
      Console.WriteLine();
      foreach (var line in tile)
        Console.WriteLine(line);
    }

    static void PrintTile(List<string> tile, Point start)
    {
      Console.SetCursorPosition(start.X, start.Y);
      foreach (var line in tile)
      {
        Console.Write(line);
        Console.SetCursorPosition(start.X, ++start.Y);
      }
    }

    static bool HasMonster(List<string> image, int x, int y)
    {
      if (image.First().Length <= monster.First().Length + y || image.Count  <= x + monster.Count)
        return false;

      for (int i = 0; i < monster.Count; i++)
        for (int j = 0; j < monster.First().Length; j++)
          if (monster[i][j] == '#' && image[x + i][y + j] != '#')
            return false;
      return true;
    }

    static List<string> monster = new List<string>(){
        "                  # ",
        "#    ##    ##    ###",
        " #  #  #  #  #  #   " };

    static void SetMonster(List<string> image, int x, int y)
    {
      for (int i = 0; i < monster.Count; i++)
      {
        StringBuilder sb = new StringBuilder(image[x + i]);

        for (int j = 0; j < monster.First().Length; j++)
        {
          if(monster[i][j] == '#')
            sb[y + j] = 'O';
        }

        image[x + i] = sb.ToString();
      }
    }

  }
}
