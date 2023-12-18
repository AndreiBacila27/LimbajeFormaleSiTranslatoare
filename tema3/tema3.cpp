#include <iostream>
#include <sstream>
#include <fstream>
#include <string>
#include <vector>
#include <map>
#include <cstdlib>
#include <ctime>
#include <iomanip>
#include <conio.h>

#include "Gramatica.h"
const int MAX_PROD = 100;
const int MAX_STARI = 100;
const int MAX_SIMBOL = 100;
using namespace std;

string prod[MAX_PROD];
string terminal;
string neterminal;
int indice_productii = 0;
int indice_stari = 0;
int salt_indice = 0;
int tabela_salt[MAX_STARI][MAX_SIMBOL];
string tabela_actiuni[MAX_STARI][MAX_SIMBOL];

class stare {
public:
	string vect[100];
	int lungime;
};

void init() {
	ifstream fin("Text.txt");
	string line;
	getline(fin, line);
	for (int i = 0; i < line.length(); i++) {
		if (line[i] != ',') {
			terminal += line[i];
		}
	}
	getline(fin, line);
	for (int i = 0; i < line.length(); i++) {
		if (line[i] != ',') {
			neterminal += line[i];
		}
	}
	while (!fin.eof()) {
		getline(fin, prod[indice_productii++]);
	
	}

	fin.close();

	for (int i = 0; i < MAX_STARI; i++)
		for (int j = 0; j < MAX_SIMBOL; j++)
			tabela_salt[i][j] = 0;

	for (int i = 0; i < MAX_STARI; i++)
		for (int j = 0; j < MAX_SIMBOL; j++)
			tabela_actiuni[i][j] = "00";
}

string urm(char x, string& urmator) {
	int pos = 0;
	for (int k = 1; k < indice_productii; k++)
		for (int j = 2; j < prod[k].length(); j++) {
			if (prod[k][j] == x) {
				if (j + 1 == prod[k].length() && prod[k][j - 1] == ' ') {
					urm(prod[k][0], urmator);
				}
				else if (j + 1 == prod[k].length()) {

				}
				else {
					urmator.push_back(prod[k][j + 1]);
				}
			}
		}
	return urmator;
}

string derivata(char x, int param) {
	int nr = -1; 
	for (int l = 0; l < indice_productii; l++) {
		if (x == prod[l][0])
		{
			nr = l + param;
			break;
		}
	}

	string raspuns = "";
	if (nr == -1)
		return raspuns;
	for (int j = 0; j < prod[nr].length(); j++) {
		if (prod[nr][j] == ' ') {
			raspuns.push_back(' ');
			raspuns.push_back('.');
		}
		else {
			raspuns.push_back(prod[nr][j]);
		}
	}
	return raspuns;

	
}

void GENINC(string v[], string element[], int& lungime, int indc) {
	element[0] = v[indc];
	int param = 0;
	lungime = 1;
	int i = 0;
	int pozitie_punct = element[0].find('.');
	if (pozitie_punct + 1 < element[0].length()) {
		while (element[i][pozitie_punct + 1] != prod[indice_productii - 1][(prod[indice_productii - 1].length()) - 1]) {
			char x;

			x = element[i][pozitie_punct + 1];

			if (neterminal.find(x) < neterminal.length()) {
				if (i == 0 && x == 'E') {
					param++;
					element[++i] = derivata(x, param);
					lungime++;
				}
				else {
					element[++i] = derivata(x, param);
					lungime++;
				}
				pozitie_punct = element[i].find('.');
				if (element[i][0] == element[i][pozitie_punct + 1])
					param++;
				else
					param = 0;
			}
			else if (x == '(') {
				param++;
				element[++i] = derivata(element[i - 1][0], param);
				lungime++;
				pozitie_punct = element[i - 1].find('.');
			}
			else {
				break;
			}
		}
		if (element[0] != "")
			indice_stari++;
	}

}

void mutare_punct_dreapta(string& stare) {
	int pozitie_punct = 0;
	char x;
	pozitie_punct = stare.find('.');
	x = stare[pozitie_punct + 1];
	stare[pozitie_punct] = x;
	stare[pozitie_punct + 1] = '.';
}



stare selecteaza_articole(stare element, char x) {
	stare el;
	el.lungime = 0;
	for (int k = 0; k < element.lungime; k++) {
		if (element.vect[k].find('.') + 1 < element.vect[k].length()) {
			if (x == element.vect[k][(element.vect[k].find('.') + 1)])
				el.vect[el.lungime++] = element.vect[k];
		}
	}
	return el;
}

string get_caractere_distincte(stare caractere_distincte) {
	string chr;
	for (int k = 0; k < caractere_distincte.lungime; k++) {
		if (caractere_distincte.vect[k].find('.') + 1 < caractere_distincte.vect[k].length()) {
			if (chr.find(caractere_distincte.vect[k][(caractere_distincte.vect[k].find('.') + 1)]) < chr.length());
			else
				chr.push_back(caractere_distincte.vect[k][(caractere_distincte.vect[k].find('.') + 1)]);
		}
	}
	return chr;
}

void GENSALT(stare& elem, char x) {
	stare elem_result;
	stare finish_result;
	finish_result.lungime = 0;
	int lng = elem.lungime;
	for (int l = 0; l < elem.lungime; l++) {
		mutare_punct_dreapta(elem.vect[l]); 
	}
	for (int k = 0; k < lng; k++) {
		GENINC(elem.vect, elem_result.vect, elem_result.lungime, k);
		for (int u = 0; u < elem_result.lungime; u++)
			finish_result.vect[finish_result.lungime++] = elem_result.vect[u];
	}
	elem = finish_result;

}

bool verificare_stari(stare st[], int ind) {
	bool eq = false;
	for (int k = 0; k < ind; k++) {
		if (st[k].lungime == st[ind].lungime) {
			for (int j = 0; j < st[k].lungime; j++) {
				if (st[k].vect[j] == st[ind].vect[j]) {
					eq = true;
				}
				else {
					eq = false;
					break;
				}
			}
		}
		if (eq == true) {
			salt_indice = k;
			break;
		}
	}
	return eq;

}


void rezolutie(stare stari[]) {
	string chr;
	int indice = 0;
	for (int n = 0; n <= indice; n++) {
		chr = get_caractere_distincte(stari[n]);
		for (int k = 0; k < chr.length(); k++) {
			stari[indice + 1] = selecteaza_articole(stari[n], chr[k]);
			GENSALT(stari[indice + 1], chr[k]);

			for (int q = 0; q < stari[indice + 1].lungime; q++) {
				if (stari[indice + 1].vect[q][stari[indice + 1].vect[q].length() - 1] == '.') {
					string productie = stari[indice + 1].vect[q];
					productie.pop_back();
					for (int z = 0; z < indice_productii; z++) {
						if (productie[productie.length() - 1] == 'E') {
							tabela_actiuni[indice + 1][terminal.length() - 1] = "ac";
							break;
						}
						if (prod[z] == productie) {
							string r = "";
							urm(productie[0], r);
							for (int c = 0; c < r.length(); c++) {
								stringstream ss;
								ss << z;
								string exp = "r" + ss.str();
								tabela_actiuni[indice + 1][terminal.find(r[c])] = exp;
								tabela_actiuni[indice + 1][terminal.length() - 1] = exp;
							}
						}
					}

				}
			}

			if (neterminal.find(chr[k]) < neterminal.length()) {
				tabela_salt[n][neterminal.find(chr[k])] = indice + 1;
			}
			else if (terminal.find(chr[k]) < terminal.length()) {
				int a = indice + 1;
				stringstream ss;
				ss << a;
				string exp = "d" + ss.str();
				tabela_actiuni[n][terminal.find(chr[k])] = exp;
			}

			if (verificare_stari(stari, indice + 1) == false) {
				indice++;
			}
			else {
				if (neterminal.find(chr[k]) < neterminal.length()) {
					tabela_salt[n][neterminal.find(chr[k])] = salt_indice;
					salt_indice = 0;
				}
				else if (terminal.find(chr[k]) < terminal.length()) {
					int a = salt_indice;
					stringstream ss;
					ss << a;
					string exp = "d" + ss.str();
					tabela_actiuni[n][terminal.find(chr[k])] = exp;
					salt_indice = 0;
				}
			}
		}
	}

}

void afisare_fisier(stare st[], int k) {
	int ct, ct2 = 0;
	ofstream fout("tabel_output.txt");
	if (!fout.is_open()) {
		cerr << "Eroare la deschiderea fișierului." << endl;
		return;
	}

;

	for (int i = 0; i < k; i++) {
		ct = 0;
		if (!st[i].vect[0].empty()) {
			for (int n = 0; n <= i; n++) {
				if (st[n].vect[0] == st[i].vect[0]) {
					ct++;
				}
			}
			if (ct == 1) {
			
				for (int j = 0; j < terminal.length(); j++)
					fout << tabela_actiuni[i][j] << "   ";
				fout << "   ";
				for (int j = 0; j < neterminal.length(); j++)
					fout << tabela_salt[i][j] << "   ";
				fout << endl;
				ct2++;
			}
		}
	}

	fout.close();
}



void main() {


	stare i[100];
	init();
	GENINC(prod, i[0].vect, i[0].lungime, 0);
	rezolutie(i);
	afisare_fisier(i, indice_stari);
	Gramatica gramatica("(a+a)*a$");
	gramatica.VerificareGramatica();
}