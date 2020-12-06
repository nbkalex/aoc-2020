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
      string content = File.ReadAllText("TextFile1.txt");
      Console.WriteLine(content.Split("\r\n\r\n").Sum(g => new HashSet<char>(g.Replace("\r\n", "")).Count()));
      Console.WriteLine(content.Split("\r\n\r\n").Sum(g => g.Split("\r\n").Aggregate((r, g1) => new string(r.Intersect(g1).ToArray())).Count() ));
    }
  }
}
