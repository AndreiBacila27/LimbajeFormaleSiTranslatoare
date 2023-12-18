using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema2
{
    class ProdSiStare
    {
        public string productie;
        public int stare;

        public ProdSiStare(string productie, int stare) {
            this.productie = productie;
            this.stare = stare;
        }
        public string getProductie()
        {
            return productie;
        }
        public int getStare()
        {
            return stare;
        }
        public override bool Equals(object? obj)
        {
            if (((ProdSiStare)obj).getProductie() == this.productie && ((ProdSiStare)obj).stare == this.stare) {
                return true;
            }
            return false;
        }
        public bool equalsStare(object? obj)
        {
            if (((ProdSiStare)obj).getStare() == this.stare){
                return true;
            }
            return false;
        }
        
    }
}
