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
    static long Evaluate(string expr, out int index, out bool aBreak)
    {
      bool b = false;
      long value = 0;
      char op = '+';
      List<int> numbers = new List<int>();
      for (int i = 0; i < expr.Length; i++)
      {
        int val = 0;
        if (expr[i] == '(')
        {
          int newPos;
          if (op == '+')
          {
            value += Evaluate(expr.Substring(i + 1), out newPos, out b);
            i += newPos + 1;
          }
          else
          {
            value *= Evaluate(expr.Substring(i + 1), out newPos, out b);
            i += newPos + 1;
          }
        }
        else if (expr[i] == ')')
        {
          index = i;
          aBreak = expr.Substring(0,i).Length == 1;
          return value;
        }
        else if (int.TryParse(expr[i].ToString(), out val))
        {
          numbers.Add(val);
          if (op == '+')
            value += val;
          else
          {
            value *= Evaluate(expr.Substring(i), out index, out b);
            i += index;

            if(b)
            {
              aBreak = false;
              index = i;
              return value;
            }
          }
        }
        else
        {
          op = expr[i];
        }
      }

      index = expr.Length;
      aBreak = b;
      return value;
    }

    static void Main(string[] args)
    {
      long sum = 0;
      foreach(var line in File.ReadAllLines("TextFile1.txt"))
      {
        string copyLine = line.Replace(" ", "");
        int closed = copyLine.IndexOf(')', 0);
        while (closed != -1)
        {
          int open = copyLine.LastIndexOf('(', closed);
          string expr = copyLine.Substring(open, closed - open+1);

          copyLine = copyLine.Replace(expr, Resolve(expr.Substring(1, expr.Length - 2)).ToString());
          closed = copyLine.IndexOf(')', 0);
        }

        sum += Resolve(copyLine);
      }

      Console.WriteLine(sum);
    }

    static long Resolve(string expr)
    {
      int i = 0;
      char op = '+';
      long value = 0;
      
      while(i < expr.Length)
      {
        if (op == '*')
        {
          value *= Resolve(expr.Substring(i));
          break;
        }
        else
        {
          string currentNumber = "";
          while (i < expr.Length && !"*+".Contains(expr[i]))
          {
            currentNumber += expr[i];
            i++;
          }

          if(i != expr.Length)
            op = expr[i];
          
          if (currentNumber != "")
            value += long.Parse(currentNumber);
          i++;
        }

      }
      return value;
    }
  }
}
