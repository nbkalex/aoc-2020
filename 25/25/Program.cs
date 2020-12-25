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
      //const long cardKey = 5764801;
      //const long doorKey = 17807724;

      const long cardKey = 14222596;
      const long doorKey = 4057428;

      long val = 1;
      int i = 0;

      while(true)
      {
        val *= 7;
        val %= 20201227;

        if (val == cardKey)
          break;

        i++;
      }

      val = 1;
      for (int j = 0; j <= i; j++)
      {
        val *= doorKey;
        val %= 20201227;
      }

      Console.WriteLine(val);
    }
  }
}

