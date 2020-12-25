using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day2
{
  class Node
  {
    public Node previous, next;
    public int val;
  }

  class Program
  {
    static void Main(string[] args)
    {
      var input = "158937462";
      var maxValue = 1000000;
      Node first = new Node();
      Node current = first;
      List<Node> nodesMap = new List<Node>(maxValue);
      for (int i = 0; i <= maxValue; i++)
        nodesMap.Add(null);

      nodesMap[(int)char.GetNumericValue(input[0])] = first;
      first.val = (int)char.GetNumericValue(input.First());
      foreach (var c in input.Skip(1))
      {
        Node newNode = new Node();
        newNode.next = first;
        newNode.previous = current;
        newNode.val = (int)char.GetNumericValue(c);
        nodesMap[newNode.val] = newNode;
        current.next = newNode;
        current = newNode;
      }

      var max = (int)char.GetNumericValue(input.Max());
      
      for (int i = max+1; i <= maxValue; i++)
      {
        Node newNode = new Node();
        newNode.next = first;
        newNode.previous = current;
        newNode.val = i;
        nodesMap[i] = newNode;
        current.next = newNode;
        current = newNode;
      }

      current = first;

      for (int i = 0; i < 10000000; i++)
      {
        Node start = current;
        List<Node> toChange = new List<Node>();
        for (int j = 0; j < 3; j++)
        {
          toChange.Add(start.next);
          start = start.next;
        }

        int val = current.val;
        int destination = val-1;

        //if(destination == 0)
        //{

        //}

        while (destination == 0 || toChange.Any(tc => tc.val == destination))
        {
          destination--;
          if (destination <= 0)
            destination = maxValue;
        }

        var next = nodesMap[destination].next;
        nodesMap[destination].next = toChange.First();
        toChange.First().previous = nodesMap[destination];
        current.next = toChange.Last().next;
        next.previous = toChange.Last();
        toChange.Last().next = next;

        //Print(nodesMap[1]);
        current = current.next;
      }

      //Print(nodesMap[1]);
      Console.WriteLine((long)nodesMap[1].next.val * nodesMap[1].next.next.val);

      //Console.WriteLine(input.Substring(input.IndexOf('1') + 1) + input.Substring(0, input.IndexOf('1')));
    }

    static void Print(Node first)
    {
      var current = first;
      while (current.next != first)
      {
        Console.Write(current.next.val);
        current = current.next;
      }
      Console.WriteLine();
    }
  }
}
