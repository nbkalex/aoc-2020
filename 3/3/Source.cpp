#include <set>
#include <fstream>
#include <iostream>
#include <string>
#include <unordered_set>
#include <unordered_map>

using namespace std;

int main()
{
  ifstream myfile;
  myfile.open("Text.txt");
    
  struct slope
  {
    int r, d, i, t;
  };

  vector<slope> slopes = {
    {1,1, 0, 0},
    {3,1, 0, 0},
    {5,1, 0, 0},
    {7,1, 0, 0},
    {1,2, 0, 0}
  };        

  string line;
  int currentDown = 0;
  while (getline(myfile, line))
  {
    int width = line.size();

    for (auto & slope : slopes)
    {
      if (currentDown % slope.d == 0)
      {
        if (line[slope.i % width] == '#')
          slope.t++;
        slope.i += slope.r;
      }
    }
    
    currentDown++;
  }

  int countAll = 1;
  for (auto slope : slopes)
    countAll *= slope.t;

  myfile.close();
}