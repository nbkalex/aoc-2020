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
      var lines = File.ReadAllLines("TextFile1.txt").ToList();
      List<Tuple<string, string>> rules = new List<Tuple<string, string>>();
      List<string> toMatch = new List<string>();
      int i = 0;
      while (i < lines.Count && lines[i] != "")
      {
        rules.Add(new Tuple<string, string>(lines[i].Split(":")[0], lines[i].Split(": ")[1]));
        i++;
      }

      i++;
      while (i < lines.Count)
      {
        toMatch.Add(lines[i]);
        i++;
      }

      int sum = 0;
      string zero = rules.FirstOrDefault(r => r.Item1 == "0").Item2;
      foreach (var tm in toMatch)
      {
        mapped.Clear();
        string copy = tm;

        bool matched = Match(rules, ref copy, zero, tm) && copy.Length == 0;

        if (matched)
        {
          Console.WriteLine(tm);
          sum++;
        }

      }

      Console.WriteLine(sum);
    }
     
    static Dictionary<string, Dictionary<string, int>> mapped = new Dictionary<string, Dictionary<string, int>>();

    static bool Match(List<Tuple<string, string>> rules, ref string toMatch, string currentRule, string initial)
    {
      string cpy = toMatch;
      if (currentRule.StartsWith('"'))
      {
        bool res = toMatch[0] == currentRule[1];
        if (res)
          toMatch = toMatch.Substring(1);
        return res;
      }

      string[] currentRulesTokens = currentRule.Split(" | ");

      foreach (var currentRulesT in currentRulesTokens)
      {
        bool matched = true;
        string toMatchCopy = toMatch;
        var currentRules = currentRulesT.Split(" ");
        foreach (var r in currentRules)
        {
          string currentRuleValue = rules.FirstOrDefault(r2 => r2.Item1 == r).Item2;
          if (toMatchCopy == "")
            break;

          bool res = Match(rules, ref toMatchCopy, currentRuleValue, initial);
          if (!res)
          {
            matched = false;
            break;
          }

          //if (toMatch == "")
          //{
          //  if (cpy == initial && )
          //}
        }

        if (matched)
        {
          toMatch = toMatchCopy;
          return true;
        }
      }

      return false;

    }
  }
}
