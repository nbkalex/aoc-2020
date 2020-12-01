#include <set>
#include <fstream>
#include <iostream>
#include <string>
#include <unordered_set>

using namespace std;

int main()
{
  ifstream myfile;
  myfile.open("Text.txt");

  vector<int> values;
  string line;
  while (getline(myfile, line))    
    values.push_back(stoi(line));

  for(int i =0; i < values.size(); i++)
    for(int j = i + 1; j < values.size(); j++)
      for (int k = j + 1; k < values.size(); k++)
      {
        if (values[i] + values[j] + values[k] == 2020)
        {
          cout << values[i] * values[j] * values[k];
          return 0;
        }
      }

  myfile.close();
}