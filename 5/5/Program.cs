using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace day2
{
  class Program
  {

    static void Main(string[] args)
    {
      
      using (StreamReader sr = new StreamReader("TextFile1.txt"))
      {
        List<int> ids = new List<int>();
        int max = 0;
        for (string line = line = sr.ReadLine(); line != null; line = sr.ReadLine())
        {
          int minRow = 0, maxRow = 127, minCol = 0, maxCol = 7;

          // FBFBBFFRLR
          foreach(var c in line)
          {
            if (c == 'B')
              minRow += (maxRow-minRow) / 2;
            if (c == 'F')
              maxRow = minRow + (maxRow - minRow) / 2;
            if (c == 'L')
              maxCol = minCol + (maxCol - minCol) / 2;
            if (c == 'R')
              minCol += (maxCol-minCol) / 2;
          }

          if (maxRow * 8 + maxCol > max)
            max = maxRow * 8 + maxCol;

          ids.Add(max = maxRow * 8 + maxCol);
        }
        ids.Sort();

        int current = ids[0];
        for (int i = 1; i < ids.Count; i++)
        {
          current++;
          if (ids[i] != current)
          {
            Console.WriteLine(ids[i]-1);
            break;
          }

        }
          
      }

    }
  }
}
