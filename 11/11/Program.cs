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
      var seats = File.ReadAllLines("TextFile1.txt").Select(line => line.ToArray()).ToArray();
      int width = seats[0].Length;
      int height = seats.Length;

      char[][] copy = { new char[0] };

      while (!seats.Zip(copy).All(s => s.First.SequenceEqual(s.Second)))
      {
        copy = seats.Select(s => (char[])s.Clone()).ToArray();
        for (int i = 0; i < height; i++)
        {
          for (int j = 0; j < width; j++)
          {
            var neighbours = GetNeighbours(copy, i, j);
            if (seats[i][j] == 'L')
            {

            }
            if (seats[i][j] == 'L' && neighbours.Count(n => n == '#') == 0)
              seats[i][j] = '#';

            if (seats[i][j] == '#' && neighbours.Count(n => n == '#') >= 5)
              seats[i][j] = 'L';

            //Console.Write(seats[i][j]);
          }
          //Console.WriteLine();
        }

        //Console.WriteLine();
        //Console.WriteLine();
      }

      Console.WriteLine(seats.Sum(line => line.Count(s => s == '#')));
    }

    static readonly (int, int)[] neighbours = new (int,int)[] { (0, 1), (1, 0), (1, 1), (0, -1), (-1, 0), (-1, -1), (-1, 1), (1, -1) };

    static List<char> GetNeighbours(char[][] seats, int i, int j)
    {
      List<char> res = new List<char>();
      foreach(var n in neighbours)
      {
        int height = i + n.Item1;
        int width = j + n.Item2;
        if (height >= 0 && width >= 0 && height < seats.Length && width < seats[0].Length)
        {
          char current = seats[height][width];
          while (height >= 0 && width >= 0 && height < seats.Length && width < seats[0].Length && current == '.')
          {
            height += n.Item1;
            width += n.Item2;

            if (height >= 0 && width >= 0 && height < seats.Length && width < seats[0].Length)
              current = seats[height][width];
          }

          if (current != '.')
            res.Add(current);
        }
      }
      return res;
    }
  }
}
