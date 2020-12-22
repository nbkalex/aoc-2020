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
      var input = File.ReadAllLines("TextFile1.txt");

      Queue<int> player1 = new Queue<int>();
      Queue<int> player2 = new Queue<int>();

      Queue<int> current = player1;
      foreach(var line in input)
      {
        if (line == "")
          current = player2;
        else
          current.Enqueue(int.Parse(line));
      }

      var copy1 = new Queue<int>(player1);
      var copy2 = new Queue<int>(player2);
      Play(copy1, copy2, 1);
      current = copy1.Count != 0 ? copy1 : copy2;
      Console.WriteLine(current.Reverse().Select((v, i) => (v, i)).Sum(n => n.v * (n.i + 1)));

      //32437 false (with end)
      //34103 false (with or)
    }

    static void Play(Queue<int> player1, Queue<int> player2, int game)
    {
      //Console.WriteLine();

      List<int[]> player1Cfg = new List<int[]>();
      List<int[]> player2Cfg = new List<int[]>();

      int round = 0;
      while (player1.Count != 0 && player2.Count != 0)
      {
        round++;
        //Console.WriteLine("--Round {0}(Game {1})--", round, game);
        //Console.WriteLine("Player 1's deck: " + string.Join(", ", player1));
        //Console.WriteLine("Player 2's deck: " + string.Join(", ", player2));
        if (player1Cfg.Any(cfg => cfg.Count() == player1.Count() && cfg.Zip(player1).All(z => z.First == z.Second)) && player2Cfg.Any(cfg => cfg.Count() == player2.Count() && cfg.Zip(player2).All(z => z.First == z.Second)))
        {
          player1.Enqueue(1);
          player2.Clear();
          return;
        }

        player1Cfg.Add(player1.ToArray());
        player2Cfg.Add(player2.ToArray());

        int p1 = player1.Dequeue();
        int p2 = player2.Dequeue();
        //Console.WriteLine("Player 1 plays: " + p1);
        //Console.WriteLine("Player 2 plays: " + p2);

        // subgame
        if (p1 <= player1.Count && p2 <= player2.Count && player1.Count != 0 && player2.Count != 0)
        {
          var copy1 = new Queue<int>(player1.Take(p1));
          var copy2 = new Queue<int>(player2.Take(p2));
          Play(copy1, copy2, game+1);
          if (copy1.Count > 0)
          {
            player1.Enqueue(p1);
            player1.Enqueue(p2);
          }
          else
          {
            player2.Enqueue(p2);
            player2.Enqueue(p1);
          }

        }
        else if (p1 > p2)
        {
          player1.Enqueue(p1);
          player1.Enqueue(p2);
          //Console.WriteLine("Player 1 wins round {0} of game {1}!", round, game);
        }
        else
        {
          player2.Enqueue(p2);
          player2.Enqueue(p1);
          //Console.WriteLine("Player 2 wins round {0} of game {1}!", round, game);
        }

        //Console.WriteLine();
      }
    }
  }
}
