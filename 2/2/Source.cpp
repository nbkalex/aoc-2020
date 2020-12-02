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

  vector<string> values;
  string line;
  while (getline(myfile, line))    
    values.push_back(line);

  myfile.close();
}