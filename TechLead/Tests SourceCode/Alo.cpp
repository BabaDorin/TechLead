#include <iostream>
#include <string>

using namespace std;
int main ()
{
	string line;
	int count = 0;	
	while (getline(cin, line)) {
	    for(int i=0; i<line.length(); i++){
		  if(line[i]==' ') count++;
		}
		break;
	}
  	count++;
  	cout<<count;
  	return 0;
}
