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

    const int rangeSize = 25;

    static void Main(string[] args)
    {
      var instructions = File.ReadAllLines("TextFile1.txt").Select(l => new { dir = l[0], dist = int.Parse(l.Substring(1)) });
      Point waypoint = new Point() { X = 10, Y = 1 };
      Point current = new Point();
      var cardinals = new Dictionary<char, Point>() { { 'E', new Point(1, 0) }, { 'N', new Point(0, 1) }, { 'S', new Point(0, -1) }, { 'W', new Point(-1, 0) } };
      var directions = new Dictionary<int, char>() { { 0, 'N' }, { 90, 'E' }, { 180, 'S' }, { 270, 'W' } };
      foreach (var inst in instructions)
      {
        int val = inst.dist;
        char dir = inst.dir;
        if (cardinals.ContainsKey(inst.dir))
        {
          waypoint.X += val * cardinals[dir].X;
          waypoint.Y += val * cardinals[dir].Y;
        }

        if (inst.dir == 'F')
        {
          current.X += waypoint.X * val;
          current.Y += waypoint.Y * val;
        }

        if (dir != 'R' && dir != 'L')
          continue;

        if (dir == 'R')
          val = 360 - val;

        if (val == 90)
        {
          var z = waypoint.X;
          waypoint.X = -waypoint.Y;
          waypoint.Y = z;
        }

        if (val == 180)
        {
          waypoint.X = -waypoint.X;
          waypoint.Y = -waypoint.Y;
        }

        if (val == 270)
        {
          var z = waypoint.X;
          waypoint.X = waypoint.Y;
          waypoint.Y = -z;
        }
      }

      Console.WriteLine(current);
      Console.WriteLine(Math.Abs(current.X) + Math.Abs(current.Y));
    }
  }
}
