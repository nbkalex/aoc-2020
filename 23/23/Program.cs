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
      var input = "158937462";
      char max = input.Max();
      char min = input.Min();

      int currentIndex = 0;

      for (int i = 0; i < 100; i++)
      {
        char current = input[currentIndex];

        int index = input.IndexOf(current) + 1;
        string next3 = "";
        for (int i2 = 0; i2 < 3; i2++)
        {
          index %= input.Length;
          next3 += input[index];
          input = input.Remove(index, 1);
        }

        int nextChar = current - 1;
        if (nextChar < min)
          nextChar = max;

        int destination = -1;
        while (destination == -1)
        {
          destination = input.IndexOf((char)nextChar);
          nextChar--;
          if (nextChar < min)
            nextChar = max;
        }

        if (destination == -1)
          destination = input.IndexOf(input.Max());

        input = input.Insert(destination + 1, next3);

        int rotate = input.IndexOf(current) - currentIndex;
        input = input.Substring(rotate) + input.Substring(0, rotate);

        currentIndex++;
        currentIndex %= input.Length;

        if(i % 10 == 0)
        {

        }
      }

      Console.WriteLine(input.Substring(input.IndexOf('1') + 1) + input.Substring(0, input.IndexOf('1')));
    }
  }
}
