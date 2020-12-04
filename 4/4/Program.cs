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
      Dictionary<string, int> passportMeta = new Dictionary<string, int>()
      {
        { "byr", 0 },
        { "iyr", 1 },
        { "eyr", 2 },
        { "hgt", 3 },
        { "hcl", 4 },
        { "ecl", 5 },
        { "pid", 6 },
        { "cid", 7 },
      };

      Dictionary<int, Predicate<string>> validation = new Dictionary<int, Predicate<string>>()
      {
        { 0, y => {int year = 0; return y != null && int.TryParse(y, out year) ? year >=1920 && year <= 2002 : false; } },
        { 1, y => {int year = 0; return y != null && int.TryParse(y, out year) ? year >=2010 && year <= 2020: false; } },
        { 2, y => {int year = 0; return y != null && int.TryParse(y, out year) ? year >=2020 && year <= 2030 : false; } },
        { 3, h => {
          if(h ==null || h.Length < 3)
            return false;
          int height = 0;
          if(!int.TryParse(h.Substring(0, h.Length - 2), out height))
            return false;

          if(h.EndsWith("cm"))
            return height <= 193 && height >= 150;

          if(h.EndsWith("in"))
            return height <= 76 && height >= 59;

          return false;
        } },
        {4, c => c!=null && c.Length == 7 && c[0] == '#' && int.TryParse(c.Substring(1), NumberStyles.HexNumber,null, out int color)},
        {5, c => c!=null && "amb blu brn gry grn hzl oth".Contains(c) },
        {6, pid => pid!= null && pid.Length == 9 && long.TryParse(pid, out long v) },
        {7, c => true  }
      };

      List<string[]> passports = new List<string[]>() { new string[8] };
      using (StreamReader sr = new StreamReader("TextFile1.txt"))
      {
        for (string line = line = sr.ReadLine(); line != null; line = sr.ReadLine())
        {
          if (line == "")
          {
            passports.Add(new string[8]);
            continue;
          }

          var data = line.Split(" ");
          foreach (var d in data)
          {
            var info = d.Split(":");
            passports.Last()[passportMeta[info[0]]] = info[1];
          }
        }
      }

      Console.WriteLine(passports.Count(pass => validation.Count(valid => valid.Value(pass[valid.Key])) == 8));
    }
  }
}
