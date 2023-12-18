#include <iostream>
#include <fstream>
#include <vector>
#include <map>
#include <sstream>
#include <stack>
#include <queue>

using namespace std;

class Gramatica {
private:
    vector<string> terminale;
    vector<string> neterminale;
    vector<string> productii;
    map<pair<int, char>, string> tabelaActiuniSiSalt;
    int randuri = 0;
    string sir = " ";

public:
    Gramatica(const string& sir) : sir(sir) {
        Citire();
        CitireTabela();
        Afisare();
        AfisareTabela();
    }

    void CitireTabela() {
        randuri = 0;
        ifstream file("C:/Users/Andre/Documents/ULBS/AN_3/LimbajeFormaleSiTranslatoare/tema3/tabel_output.txt");
        if (file.is_open()) {
            string line;
            while (getline(file, line)) {
                int j = 0;
                istringstream iss(line);
                string token;
                while (iss >> token) {
                    
                    if (token.size() == neterminale.size() + terminale.size() + 1) {
                        for (const auto& s : terminale) {
                            char terminal_char = s[0];
                            tabelaActiuniSiSalt[{randuri, terminal_char}] = token[j++];
                        }
                        char dolar_char = '$';
                        tabelaActiuniSiSalt[{randuri, dolar_char}] = token[j++];
                        for (const auto& s : neterminale) {
                            char neterminal_char = s[0];
                            tabelaActiuniSiSalt[{randuri, neterminal_char}] = token[j++];
                        }
                        randuri++;
                    }
                    

                }
            }
            file.close();
        }
        else {
            cout << "Nu se poate deschide fisierul\n";
        }
    }

    void AfisareTabela() {
        for (int i = 0; i < randuri; i++) {
            if (i < 10)
                cout << i << " |";
            else
                cout << i << "|";
            for (const auto& s : terminale)
                cout << tabelaActiuniSiSalt[make_pair(i, s[0])] << "   ";
            cout << tabelaActiuniSiSalt[make_pair(i, '$')] << "   ";
            for (const auto& s : neterminale)
                cout << tabelaActiuniSiSalt[make_pair(i, s[0])] << "   ";
            cout << "\n";
        }
    }

    void Citire() {
        int i = 0;
        ifstream file("Text.txt");
        if (file.is_open()) {
            string line;
            while (getline(file, line)) {
                switch (i) {
                case 0:
                    terminale = Split(line, ',');

                    break;

                case 1:
                    neterminale = Split(line, ',');

                    break;

                default:
                    productii.push_back(line);
                    break;
                }
                i++;
            }
            file.close();
        }
        else {
            cout << "Nu se poate deschide fisierul\n";
        }
    }

    void Afisare() {
        cout << "Neterminale sunt: ";
        for (const auto& n : neterminale)
            cout << n << " ";
        cout << "\n";

        cout << "Terminale sunt: ";
        for (const auto& t : terminale)
            cout << t << " ";
        cout << "\n";

        for (const auto& t : productii)
            cout << t << "\n";
        cout << "\n";
    }

    void VerificareGramatica() {
        queue<char> queue;
        stack<string> stack;
        for (char s : sir)
            queue.push(s);
        stack.push("$");
        stack.push("0");
        char caracter = queue.front();
        try {
            while (tabelaActiuniSiSalt[make_pair(stoi(stack.top()), caracter)] != "00" &&
                tabelaActiuniSiSalt[make_pair(stoi(stack.top()), caracter)] != "ac") {

                string actiune = tabelaActiuniSiSalt[make_pair(stoi(stack.top()), caracter)];
                char actiune_tip = actiune[0];

                if (actiune_tip == 'r') {
                    int vf = stoi(stack.top());
                    int nrexpr = stoi(actiune.substr(1)) - 1;
                    vector<string> termeni = Split(productii[nrexpr], ' ');
                    char neterminal = termeni[0][0];
                    int nr = termeni[1].size() * 2;
                    while (nr > 0) {
                        nr--;
                        stack.pop();
                    }
                    string n = tabelaActiuniSiSalt[make_pair(stoi(stack.top()), neterminal)];
                    stack.push(string(1, neterminal));
                    stack.push(n);
                }
                else if (actiune_tip == 'd') {
                    int vf2 = stoi(stack.top());
                    stack.push(string(1, caracter));
                    stack.push(actiune.substr(1));
                    queue.pop();
                    caracter = queue.front();
                }
                else {
                }
            }

            string actiune = tabelaActiuniSiSalt[make_pair(stoi(stack.top()), caracter)];
            if (actiune == "00") {
                cout << "EROARE!\n";
            }
            else if (actiune == "ac") {
                cout << "Acceptat\n";
                stack.push(string(1, queue.front()));
            }
        }
        catch (const exception& ex) {
            cerr << "EROARE\n";
        }
    }

    vector<string> Split(const string& input, char delimiter) {
        vector<string> result;
        istringstream stream(input);
        string token;
        while (getline(stream, token, delimiter)) {
            result.push_back(token);
        }
        return result;
    }
};