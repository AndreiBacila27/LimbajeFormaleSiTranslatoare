using tema2;

Stack<ProdSiStare> stack = new Stack<ProdSiStare>();
Stiva stiva = new Stiva(stack);
string path = "C:\\Users\\Andre\\Documents\\ULBS\\AN_3\\LimbajeFormaleSiTranslatoare\\tema2\\output.txt";
TestareCuvant ex = new TestareCuvant("(a+a+a)*(a+a)$",stiva, path);
ex.Tabela(path);

string sir = "a*-(a-a)+a*a$";



