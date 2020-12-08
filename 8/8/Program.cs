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
      string[] content = File.ReadAllLines("TextFile1.txt");
      
      bool valid = false;
      int index = 0;
      while (!valid)
      {
        var programCopy = (string[])content.Clone();
         if (programCopy[index].StartsWith("nop"))
          programCopy[index] = programCopy[index].Replace("nop", "jmp");
        else if (programCopy[index].StartsWith("jmp"))
          programCopy[index] = programCopy[index].Replace("jmp", "nop");
        else
        {
          index++;
          continue;
        }

        int acc = 0;
        valid = true;
        HashSet<int> program = new HashSet<int>();
        for (int i = 0; i < programCopy.Length; i++)
        {
          string inst = programCopy[i];
          if (!program.Add(i))
          {
            valid = false;
            break;
          }

          string[] instruction = inst.Split(" ");
          int arg = int.Parse(instruction[1]);
          if (instruction[0] == "acc")
            acc += arg;
          if (instruction[0] == "jmp")
            i += arg - 1;
        }

        index++;

        if (valid)
          Console.WriteLine(acc);
      }
    }
  }
}
