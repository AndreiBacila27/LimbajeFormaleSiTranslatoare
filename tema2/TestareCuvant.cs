using GramaticiLR1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace tema2
{
    class TestareCuvant
    {
        string cuvIntrare;
        Stiva stiva;
        Dictionar dictionar;
        string filePath;
        private List<string> terminale = new();
        private List<string> neterminale = new();
        private readonly List<string> productii = new();
        private Dictionary<(int, char), string> tabelaActiuni = new Dictionary<(int, char), string>();
        private Dictionary<(int, char), string> tabelaSalt = new Dictionary<(int, char), string>();

        public Dictionary<ProdSiStare, string> dictionarTActiuni = new Dictionary<ProdSiStare, string>();
        public Dictionary<ProdSiStare, int> dictionarTSalt = new Dictionary<ProdSiStare, int>();
        public Dictionary<ProdSiStare, string> productii2 = new Dictionary<ProdSiStare, string>();


        private int randuri = 0;

        public TestareCuvant(string cuvIntrare, Stiva stiva, string filePath)
        {
            this.cuvIntrare = cuvIntrare;
            this.stiva = stiva;
            this.filePath= filePath;
            Citire();
            Afisare();
            CitireTabela(filePath);
            dictionar = new Dictionar(dictionarTActiuni, dictionarTSalt,productii2);
            //AfisareTabelaActiuni();
           // AfisareTabelaSalt();

        }
        public void Tabela(string filePath)
        {
            Tabela tabela = new(terminale, neterminale, productii);
            tabela.LoadGrammar(filePath);
        }


        public void Citire()
        {
            int i = 0;
            int j = 0;
            foreach (string line in File.ReadLines("C:\\Users\\Andre\\Documents\\ULBS\\AN_3\\LimbajeFormaleSiTranslatoare\\tema2\\date.txt"))
            {
                switch (i)
                {
                    case 0:
                        neterminale = line.Split(",").ToList();
                        break;

                    case 1:
                        terminale = line.Split(",").ToList();
                        break;

                    default:
                        productii.Add(line);
                        string[] parts = line.Split("->");
                        if (parts.Length == 2)
                        {
                            j++;
                            string neterminal = parts[0].Trim();
                            string productie = parts[1].Trim();
                            var prodSiStare = new ProdSiStare(neterminal, j);
                            productii2[prodSiStare] = productie;
                        }
                        break;
                }
                i++;
            }
        }
   

        public void CitireTabela(string filePath)
        {
            randuri = 0;

            foreach (string line in File.ReadLines(filePath))
            {
                int j = 0;
                string[] cuv = line.Split(" ");
                if (cuv.Length == neterminale.Count + terminale.Count + 1)
                {
                    foreach (string s in terminale)
                    {
                        var keyActiuni = new ProdSiStare(s, randuri);
                        dictionarTActiuni[keyActiuni] = cuv[j++];
                    }
                    var dollarKeyActiuni = new ProdSiStare("$", randuri);
                 dictionarTActiuni[dollarKeyActiuni] = cuv[j++];

                    foreach (string s in neterminale)
                    {
                        var keySalt = new ProdSiStare(s, randuri);
                      dictionarTSalt[keySalt] = Int32.Parse(cuv[j++]);
                    }
                    randuri++;
                }
                else
                {
                    Console.WriteLine("Matricea introdusa nu este corecta");
                }
            }
        }


        public void AfisareTabelaActiuni()
        {
            Console.Write("   ");
            foreach (string s in terminale)
                Console.Write("{0}    ", s);
            Console.Write("$    ");
          
            Console.WriteLine();
            for (int i = 0; i < randuri; i++)
            {

                
                    if (i < 10)
                        Console.Write("{0} |", i);
                    else
                        Console.Write("{0}|", i);

                    foreach (string s in terminale)
                    {
                        var key = (i, Char.Parse(s));
                        if (tabelaActiuni.ContainsKey(key))
                            Console.Write("{0}   ", tabelaActiuni[key]);
                        else
                            Console.Write("Key not found   ");
                    }

                    var dollarKey = (i, '$');
                    if (tabelaActiuni.ContainsKey(dollarKey))
                        Console.Write("{0}   ", tabelaActiuni[dollarKey]);
                    else
                        Console.Write("Key not found   ");


                    Console.WriteLine();
                

            }
        }

        public void AfisareTabelaSalt()
        {
            Console.Write("   ");
           
            foreach (string s in neterminale)
                Console.Write("{0}   ", s);
            Console.WriteLine();
            for (int i = 0; i < randuri; i++)
            {
                if (i < 10)
                    Console.Write("{0} |", i);
                else
                    Console.Write("{0}|", i);


               

                foreach (string s in neterminale)
                {
                    var key = (i, Char.Parse(s));
                    if (tabelaSalt.ContainsKey(key))
                        Console.Write("{0}   ", tabelaSalt[key]);
                    else
                        Console.Write("Key not found   ");
                }

                Console.WriteLine();
            }
        }


        public void Afisare()
        {
            Console.Write("Neterminale sunt: ");
            foreach (string n in neterminale)
                Console.Write("{0} ", n);
            Console.WriteLine();
            Console.Write("Terminale sunt: ");
            foreach (string t in terminale)
                Console.Write("{0} ", t);
            Console.WriteLine();
            foreach (string t in productii)
                Console.WriteLine("{0}", t);
            Console.WriteLine();
        }


        public void Deplasare(int stare)
        {
            Console.WriteLine("deplasare");
            string primulElement = cuvIntrare.Substring(0, 1);
            stiva.AdaugaInStiva(new ProdSiStare(primulElement, stare));
            cuvIntrare = cuvIntrare.Substring(1);
            stiva.AfisareStiva();
        }
    

        public void Reducere(int stare)
        {
            Console.WriteLine("reducere");
            int lungimeProdDr = dictionar.cautareProdDreapta(stare).Length;
   
            string productieStanga = dictionar.cautareProdStanga(stare);
            for (int i = 0; i < lungimeProdDr; i++)
            {
                stiva.EliminareDinStiva();
            }
            ProdSiStare ps = stiva.EliminareDinStiva();
            int stareComparatie = ps.stare;
            stiva.AdaugaInStiva(ps);
            int stareRez = dictionar.cautareTS(new ProdSiStare(productieStanga, stareComparatie));
            ProdSiStare rez = new ProdSiStare(productieStanga, stareRez);
            stiva.AdaugaInStiva(rez);
        }
        public int formareStiva()
        {
            stiva.AfisareStiva();
            Console.WriteLine("Cuvantul de intrare: " + cuvIntrare);
            ProdSiStare elementCurentStiva = stiva.EliminareDinStiva();
            stiva.AdaugaInStiva(elementCurentStiva);
            int stare = elementCurentStiva.stare;

            string caracter = cuvIntrare.Substring(0, 1);
            string actiune = dictionar.cautareTA(new ProdSiStare(caracter, stare));
            int stare2 = 0;
            if (actiune.Substring(0, 1) == "d")
            {
                if (actiune.Length >= 2)
                {
                    try
                    {
                        stare2 = Int32.Parse(actiune.Substring(1, actiune.Length - 1));
                    }
                    catch (Exception ex)

                    {
                        Console.WriteLine("Format int invalid!");
                    }
                }
                Deplasare(stare2);
            }
            else
            if (actiune.Substring(0, 1) == "r")
            {
                if (actiune.Length >= 2)
                {
                    try
                    {
                        stare2 = Int32.Parse(actiune.Substring(1, actiune.Length - 1));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Format int invalid!");
                    }

                }
                Reducere(stare2);
            }
            else
            if (actiune == "ac")
            {

                return 1;
            }
            else
            if (actiune == "00")
            {
                return 2;
            }
            return 0;
        }

        public void evolutie()
        {
            int rezStiva = 0;
            while (cuvIntrare.Length > 0 && rezStiva == 0)
            {
                rezStiva = formareStiva();
            }
            if (rezStiva == 1)
            {
                Console.WriteLine("Cuvant acceptat!");
            }else
            if (rezStiva == 2)
            {
                Console.WriteLine("Eroare!");
            }
        }
    }
}
