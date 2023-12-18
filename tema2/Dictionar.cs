using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema2
{
    class Dictionar
    {
        public Dictionary<ProdSiStare, string> dictionarTActiuni = new Dictionary<ProdSiStare, string>();
        public Dictionary<ProdSiStare, int> dictionarTSalt = new Dictionary<ProdSiStare, int>();
        public Dictionary<ProdSiStare, string> productii = new Dictionary<ProdSiStare, string>();

        public Dictionar()
        {
            adaugaTActiuni();
            adaugaTSalt();
            adaugaProductii();
        }
        public void adaugaTActiuni()
        {
            dictionarTActiuni.Add(new ProdSiStare("a", 0), "d5");
            dictionarTActiuni.Add(new ProdSiStare("a", 1), "err");
            dictionarTActiuni.Add(new ProdSiStare("a", 2), "err");
            dictionarTActiuni.Add(new ProdSiStare("a", 3), "err");
            dictionarTActiuni.Add(new ProdSiStare("a", 4), "d5");
            dictionarTActiuni.Add(new ProdSiStare("a", 5), "err");
            dictionarTActiuni.Add(new ProdSiStare("a", 6), "d5");
            dictionarTActiuni.Add(new ProdSiStare("a", 7), "d5");
            dictionarTActiuni.Add(new ProdSiStare("a", 8), "err");
            dictionarTActiuni.Add(new ProdSiStare("a", 9), "err");
            dictionarTActiuni.Add(new ProdSiStare("a", 10), "err");
            dictionarTActiuni.Add(new ProdSiStare("a", 11), "err");

            dictionarTActiuni.Add(new ProdSiStare("+", 0), "err");
            dictionarTActiuni.Add(new ProdSiStare("+", 1), "d6");
            dictionarTActiuni.Add(new ProdSiStare("+", 2), "r2");
            dictionarTActiuni.Add(new ProdSiStare("+", 3), "r4");
            dictionarTActiuni.Add(new ProdSiStare("+", 4), "err");
            dictionarTActiuni.Add(new ProdSiStare("+", 5), "r6");
            dictionarTActiuni.Add(new ProdSiStare("+", 6), "err");
            dictionarTActiuni.Add(new ProdSiStare("+", 7), "err");
            dictionarTActiuni.Add(new ProdSiStare("+", 8), "d6");
            dictionarTActiuni.Add(new ProdSiStare("+", 9), "r1");
            dictionarTActiuni.Add(new ProdSiStare("+", 10), "r3");
            dictionarTActiuni.Add(new ProdSiStare("+", 11), "r5");

            dictionarTActiuni.Add(new ProdSiStare("*", 0), "err");
            dictionarTActiuni.Add(new ProdSiStare("*", 1), "err");
            dictionarTActiuni.Add(new ProdSiStare("*", 2), "d7");
            dictionarTActiuni.Add(new ProdSiStare("*", 3), "r4");
            dictionarTActiuni.Add(new ProdSiStare("*", 4), "err");
            dictionarTActiuni.Add(new ProdSiStare("*", 5), "r6");
            dictionarTActiuni.Add(new ProdSiStare("*", 6), "err");
            dictionarTActiuni.Add(new ProdSiStare("*", 7), "err");
            dictionarTActiuni.Add(new ProdSiStare("*", 8), "err");
            dictionarTActiuni.Add(new ProdSiStare("*", 9), "d7");
            dictionarTActiuni.Add(new ProdSiStare("*", 10), "r3");
            dictionarTActiuni.Add(new ProdSiStare("*", 11), "r5");

            dictionarTActiuni.Add(new ProdSiStare("(", 0), "d4");
            dictionarTActiuni.Add(new ProdSiStare("(", 1), "err");
            dictionarTActiuni.Add(new ProdSiStare("(", 2), "err");
            dictionarTActiuni.Add(new ProdSiStare("(", 3), "err");
            dictionarTActiuni.Add(new ProdSiStare("(", 4), "d4");
            dictionarTActiuni.Add(new ProdSiStare("(", 5), "err");
            dictionarTActiuni.Add(new ProdSiStare("(", 6), "d4");
            dictionarTActiuni.Add(new ProdSiStare("(", 7), "d4");
            dictionarTActiuni.Add(new ProdSiStare("(", 8), "err");
            dictionarTActiuni.Add(new ProdSiStare("(", 9), "err");
            dictionarTActiuni.Add(new ProdSiStare("(", 10), "err");
            dictionarTActiuni.Add(new ProdSiStare("(", 11), "err");

            dictionarTActiuni.Add(new ProdSiStare(")", 0), "err");
            dictionarTActiuni.Add(new ProdSiStare(")", 1), "err");
            dictionarTActiuni.Add(new ProdSiStare(")", 2), "r2");
            dictionarTActiuni.Add(new ProdSiStare(")", 3), "r4");
            dictionarTActiuni.Add(new ProdSiStare(")", 4), "err");
            dictionarTActiuni.Add(new ProdSiStare(")", 5), "r6");
            dictionarTActiuni.Add(new ProdSiStare(")", 6), "err");
            dictionarTActiuni.Add(new ProdSiStare(")", 7), "err");
            dictionarTActiuni.Add(new ProdSiStare(")", 8), "d11");
            dictionarTActiuni.Add(new ProdSiStare(")", 9), "r1");
            dictionarTActiuni.Add(new ProdSiStare(")", 10), "r3");
            dictionarTActiuni.Add(new ProdSiStare(")", 11), "r5");

            dictionarTActiuni.Add(new ProdSiStare("$", 0), "err");
            dictionarTActiuni.Add(new ProdSiStare("$", 1), "acc");
            dictionarTActiuni.Add(new ProdSiStare("$", 2), "r2");
            dictionarTActiuni.Add(new ProdSiStare("$", 3), "r4");
            dictionarTActiuni.Add(new ProdSiStare("$", 4), "err");
            dictionarTActiuni.Add(new ProdSiStare("$", 5), "r6");
            dictionarTActiuni.Add(new ProdSiStare("$", 6), "err");
            dictionarTActiuni.Add(new ProdSiStare("$", 7), "err");
            dictionarTActiuni.Add(new ProdSiStare("$", 8), "err");
            dictionarTActiuni.Add(new ProdSiStare("$", 9), "r1");
            dictionarTActiuni.Add(new ProdSiStare("$", 10), "r3");
            dictionarTActiuni.Add(new ProdSiStare("$", 11), "r5");
        }

        public void adaugaTSalt()
        {
            dictionarTSalt.Add(new ProdSiStare("E", 0), 1);
            dictionarTSalt.Add(new ProdSiStare("E", 1), 0);
            dictionarTSalt.Add(new ProdSiStare("E", 2), 0);
            dictionarTSalt.Add(new ProdSiStare("E", 3), 0);
            dictionarTSalt.Add(new ProdSiStare("E", 4), 8);
            dictionarTSalt.Add(new ProdSiStare("E", 5), 0);
            dictionarTSalt.Add(new ProdSiStare("E", 6), 0);
            dictionarTSalt.Add(new ProdSiStare("E", 7), 0);
            dictionarTSalt.Add(new ProdSiStare("E", 8), 0);
            dictionarTSalt.Add(new ProdSiStare("E", 9), 0);
            dictionarTSalt.Add(new ProdSiStare("E", 10), 0);
            dictionarTSalt.Add(new ProdSiStare("E", 11), 0);

            dictionarTSalt.Add(new ProdSiStare("T", 0), 2);
            dictionarTSalt.Add(new ProdSiStare("T", 1), 0);
            dictionarTSalt.Add(new ProdSiStare("T", 2), 0);
            dictionarTSalt.Add(new ProdSiStare("T", 3), 0);
            dictionarTSalt.Add(new ProdSiStare("T", 4), 2);
            dictionarTSalt.Add(new ProdSiStare("T", 5), 0);
            dictionarTSalt.Add(new ProdSiStare("T", 5), 0);
            dictionarTSalt.Add(new ProdSiStare("T", 6), 9);
            dictionarTSalt.Add(new ProdSiStare("T", 7), 0);
            dictionarTSalt.Add(new ProdSiStare("T", 8), 0);
            dictionarTSalt.Add(new ProdSiStare("T", 9), 0);
            dictionarTSalt.Add(new ProdSiStare("T", 10), 0);
            dictionarTSalt.Add(new ProdSiStare("T", 11), 0);

            dictionarTSalt.Add(new ProdSiStare("F", 0), 3);
            dictionarTSalt.Add(new ProdSiStare("F", 1), 0);
            dictionarTSalt.Add(new ProdSiStare("F", 2), 0);
            dictionarTSalt.Add(new ProdSiStare("F", 3), 0);
            dictionarTSalt.Add(new ProdSiStare("F", 4), 3);
            dictionarTSalt.Add(new ProdSiStare("F", 5), 0);
            dictionarTSalt.Add(new ProdSiStare("F", 6), 3);
            dictionarTSalt.Add(new ProdSiStare("F", 7), 10);
            dictionarTSalt.Add(new ProdSiStare("F", 8), 0);
            dictionarTSalt.Add(new ProdSiStare("F", 9), 0);
            dictionarTSalt.Add(new ProdSiStare("F", 10), 0);
            dictionarTSalt.Add(new ProdSiStare("F", 11), 0);
        }
        public void adaugaProductii()
        {
            productii.Add(new ProdSiStare("E", 1), "E+T");
            productii.Add(new ProdSiStare("E", 2), "T");
            productii.Add(new ProdSiStare("T", 3), "T*F");
            productii.Add(new ProdSiStare("T", 4), "F");
            productii.Add(new ProdSiStare("F", 5), "(E)");
            productii.Add(new ProdSiStare("F", 6), "a");
            productii.Add(new ProdSiStare("F", 7), "-(E)");
        }

        public string cautareTA(ProdSiStare prodSiStare) 
        {
            foreach (KeyValuePair<ProdSiStare, string> item in dictionarTActiuni) 
            {
                if (item.Key.Equals(prodSiStare)) 
                {
                    return item.Value;
                }

            }
            Console.WriteLine("Nu exista!");
            return null;
        }
        
        public int cautareTS(ProdSiStare prodSiStare)
        {
            foreach (KeyValuePair<ProdSiStare, int> item in dictionarTSalt)
            {
                if (item.Key.Equals(prodSiStare))
                {
                    return item.Value;
                }
            }
            Console.WriteLine("Nu exista!");
            return -1;
        }

        public string cautareProdDreapta(int stare)
        {
            foreach(KeyValuePair<ProdSiStare, string> item in productii)
            {
                if (item.Key.stare==stare)
                {
                    return item.Value;
                }
            }
            return null;
        }
        public string cautareProdStanga(int stare)
        {
            foreach (KeyValuePair<ProdSiStare, string> item in productii)
            {
                if (item.Key.stare == stare)
                {
                    return item.Key.getProductie();
                }
            }
            return null;
        }
    }
}
