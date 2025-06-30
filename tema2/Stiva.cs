using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema2
{
    class Stiva
    {
        Stack<ProdSiStare>stiva = new Stack<ProdSiStare>();

        public Stiva(Stack<ProdSiStare> stiva) 
        {
            this.stiva = stiva;
            InitializareStiva();
        }
        public Stiva()
        {
          
        }

        public void InitializareStiva() 
        {
            stiva.Push(new ProdSiStare("$", 0));
        }
        public void AdaugaInStiva(ProdSiStare prodSiStare) 
        {
            stiva.Push(prodSiStare);
        }

        public ProdSiStare EliminareDinStiva() 
        {
           ProdSiStare prodSiStare = stiva.Pop();
           return prodSiStare;
        }
        public void AfisareStiva() 
        {
            foreach (ProdSiStare item in stiva)
            {
               // Console.Write(item.productie + item.stare);
            }
            Console.WriteLine();
        }
        public int MarimeStiva()
        {
            return stiva.Count;
        }
    }
}
