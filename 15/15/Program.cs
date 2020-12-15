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
      List<int> sequence = new List<int>() { 0, 1, 4, 13, 15, 12, 16 };
      var last = sequence.SkipLast(1).Select((n, i) => (n, i)).ToDictionary(n => n.n, n => n.i + 1);
      while (sequence.Count < 30000000)
      {
        int index = last.ContainsKey(sequence.Last()) ? last[sequence.Last()] : 0;
        last[sequence.Last()] = sequence.Count;

        sequence.Add(index == 0 ? index : sequence.Count - index);
      }

      //sequence.Reverse();
      Console.WriteLine(sequence.Last());
    }
  }
}
