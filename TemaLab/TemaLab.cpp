#include <iostream>
#include <fstream>
#include <string>
#include <algorithm>
using namespace std;

void desparte(string sursa, char destinatie[], int &destinatie_size)
{
	destinatie_size = 0;
	for (int i = 0; i < sursa.length(); i++) 
	{
		if (sursa[i] != ' ')
		{
			destinatie[destinatie_size] = sursa[i];
			destinatie_size++;
		}
	}
}

void desparte_matrice(string sursa, string productii[50][50], int& nrLinii)
{
	string aux;
	int col = 0;
	nrLinii = 0;
	for (int i = 0; i < sursa.length(); i++) 
	{
		aux = "";
		while (sursa[i] != ' ' && i<sursa.length()) 
		{
			aux += sursa[i];
			i++;
		}
		productii[nrLinii][col] = aux;
		col++;

		if (col == 2)
		{
			nrLinii++;
			col = 0;
		}
	}
}

int verificare_neterminale(char neterminale[], string derivate)
{
	for (int i = 0;i<derivate.length();i++)
	{
		for(int j = 0;j<3;j++)
		{
			if (neterminale[j] == derivate[i]) 
			{
				return i;
			}
		}
	}
	return -1;
}

string transformareE(string productii[50][50], int random) 
{
	if (random == 0) 
	{
		return productii[0][0];
	}
	else if (random == 1) 
	{
		return productii[0][1];
	}
}

string transformareT(string productii[50][50], int random)
{
	if (random == 0)
	{
		return productii[1][0];
	}
	else if (random == 1)
	{
		return productii[1][1];
	}
}

string transformareF(string productii[50][50], int random)
{
	if (random == 0)
	{
		return productii[2][0];
	}
	else if (random == 1)
	{
		return productii[2][1];
	}
}

int main()
{
	char text;
	char neterminale[3];
	int neterminale_size;
	char terminale[5];
	int terminale_size;
	string productii[50][50];
	int productii_linie;
	string derivate;
	int derivate_size;
	int random;
	int sir_size=0;
	int linie_fisier = 0;
	string s;
	ifstream f("file.txt");

	srand(static_cast<unsigned>(time(nullptr)));

	while (getline(f, s))
	{	
		if (linie_fisier == 0) {
			desparte(s, neterminale, neterminale_size);
		}
		else if (linie_fisier == 1)
		{
			desparte(s, terminale, terminale_size);
		}
		else if (linie_fisier == 2) 
		{ 
			desparte_matrice(s,productii,productii_linie); 
		}
		else derivate = s;
		linie_fisier++;
	}
	
	while (derivate.length() < 60 && verificare_neterminale(neterminale, derivate) != -1)
	{
		cout << derivate << endl;

		random = rand() % 2;
		int f = verificare_neterminale(neterminale, derivate);
		if (derivate[f] == 'E') {
			string rezE = transformareE(productii, random);
			//derivate.replace(f, transformareE(productii, random).length(), transformareE(productii, random));
			derivate.erase(f,1);
			derivate.insert(f, rezE);
		}
		else if (derivate[f] == 'T') {
			string rezT = transformareT(productii, random);
			//derivate.replace(f, transformareT(productii, random).length(), transformareT(productii, random));
			derivate.erase(f,1);
			derivate.insert(f, rezT);
		}
		else if (derivate[f] == 'F') {
			string rezF = transformareF(productii, random);
			//derivate.replace(f, transformareF(productii, random).length(), transformareF(productii, random));
			derivate.erase(f,1);
			derivate.insert(f, rezF);
		}

	}
	cout <<"sir final: "<< derivate;
	
}