using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace day2
{
  class Program
  {

    const int rangeSize = 25;

    static void Main(string[] args)
    {
      var lines = File.ReadAllLines("TextFile1.txt");

      string mask = "";
      
       Dictionary<long, long> mem = new Dictionary<long, long>();
      foreach(var l in lines)
      {
        string[] tokens = l.Split(" = ");
        if (l.StartsWith("mask"))
          mask = tokens[1];
        else if(l.StartsWith("mem["))
        {          
          string adress = Convert.ToString(long.Parse(tokens[0].Substring(4, tokens[0].Length - 5)), 2);
          long val = long.Parse(tokens[1]);
          string maskedAdress = new string(
            adress.PadLeft(mask.Length, '0')
            .Zip(mask)
            .Select(c => c.Second == '0' ? c.First : c.Second == '1' ? '1' : 'X')
            .ToArray());

          var xs = mask.Select((c,i) => (c,i))                       
                       .Where(c => c.c == 'X')
                       .Select(c => c.i)
                       .ToArray();

          for(int n = 0; n < Math.Pow(2, xs.Count()); n++)
          {
            var newAdr = new StringBuilder(new string(maskedAdress.ToArray()));
            string current = Convert.ToString(n, 2).PadLeft(xs.Length, '0');
            current.Select((c, i) => newAdr[xs[i]] = c).ToArray();
            mem[Convert.ToInt64(newAdr.ToString(), 2)] = val;
          }
        }
      }

      
      Console.WriteLine(mem.Values.Sum());

      // 173755762235
    }

    static bool CheckVal(string mask, long current, string adress)
    {
      return Convert.ToString(current, 2)
        .PadLeft(mask.Length, '0')
        .Zip(mask)
        .Zip(adress)
        .All(c => (c.First.Second == 'X') 
               || (c.First.Second == '0' && c.First.First == c.Second) 
               || (c.First.Second == '1' && c.First.First == '1'));
    }
  }
}
