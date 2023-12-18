using Data;
using System.Data;

namespace GramaticiLR1
{
    internal class Tabela
    {
        private string Neterminals = "", Terminals = "";
        private readonly List<DataStruct> Productions = new();
        private List<List<DataStruct>> Collections = new();
        private readonly List<Colection> f = new();
        private readonly List<Colection> P1 = new();
        private readonly DataTable tabelaActiuniSiSalt = new();

        public Tabela(List<string> terminale, List<string> neterminale, List<string> productii)
        {
            foreach (string terminal in terminale)
            {
                Terminals += terminal;
            }
            foreach (string neterminal in neterminale)
            {
                Neterminals += neterminal;
            }
            foreach (string progres in productii)
            {
                this.Productions.Add(new DataStruct(progres.Split("->")[0], progres.Split("->")[1]));
            };
        }

        public void LoadGrammar(string filePath)
        {
            f.Clear();
            Collections = Colectie();
            LoadData(filePath);
        }

        private List<DataStruct> Inc(List<DataStruct> list_inc)
        {
            List<DataStruct> M0 = new();
            List<DataStruct> M1 = new();
            List<DataStruct> productii = new(Productions);

            M0 = new List<DataStruct>(list_inc);
            bool firstStep = true;
            DataStruct aux;
            do
            {
                aux = M0[0];
                M0.RemoveAt(0);
                int poz = aux.value.IndexOf(".");
                if (poz < aux.value.Length - 1 && !Terminals.Contains(aux.value.Substring(aux.value.IndexOf(".") + 1, 1)))
                {
                    if (!firstStep)
                    {
                        // da remove ca sa nu mai poata fi luata inca o data la find, dar cred ca era cu .
                        productii.RemoveAll(x => x.value == (poz != -1 ? (aux.value[0..poz] + aux.value[(poz + 1)..]) : aux.value));
                    }
                    DataStruct? S = productii.Find(x => x.key == aux.value.Substring(aux.value.LastIndexOf(".") + 1, 1));
                    if (S != null)
                        M0.Add(new DataStruct(S.key, "." + S.value));
                    M1.Add(aux);
                    firstStep = false;
                }
                else
                {
                    if (!firstStep)
                    {
                        List<DataStruct> ceva = new(productii.FindAll(x => x.key == aux.key));
                        List<DataStruct> insertPoint = new();
                        foreach (var item in ceva)
                        {
                            insertPoint.Add(new(item.key, item.value));
                        }

                        foreach (DataStruct aux3 in insertPoint)
                        {
                            aux3.value = "." + aux3.value;
                            M1.Add(aux3);
                        }
                    }
                    else
                    {
                        foreach (DataStruct aux3 in list_inc)
                        {
                            M1.Add(aux3);
                        }
                    }
                    M0.Clear();
                    firstStep = false;
                }
            }
            while (M0.Count > 0);
            M1 = M1.Distinct().ToList();
            return M1;
        }

        private List<DataStruct> Jump(List<DataStruct> I, string x)
        {
            List<DataStruct> I1 = new();
            foreach (var item in I)
            {
                I1.Add(new DataStruct(item.key, item.value));
            }
            List<DataStruct> M = new();
            foreach (var item in I1)
            {
                int poz = item.value.IndexOf('.');
                if (poz < item.value.Length - 1)
                {
                    if (item.value.Substring(poz + 1, 1) == x)
                    {
                        item.value = item.value.Remove(poz, 1);
                        item.value = item.value.Insert(++poz, ".");
                        M.Add(item);
                    }
                }
            }
            M = Inc(M);
            return M;
        }

        private List<List<DataStruct>> Colectie()
        {
            int currentIndex = 0;

            List<List<DataStruct>> C = new List<List<DataStruct>>();
            List<List<DataStruct>> I = new List<List<DataStruct>>();

            C.Add(Inc(new List<DataStruct> { new DataStruct("S", ".E") }));
            I.Add(C[0]);

            while (currentIndex < I.Count)
            {
                List<string> symbols = new List<string>();

                foreach (var item in C[currentIndex])
                {
                    int dotIndex = item.value.IndexOf('.') + 1;
                    if (dotIndex < item.value.Length)
                    {
                        string symbol = item.value.Substring(dotIndex, 1);
                        if (!symbols.Contains(symbol))
                        {
                            symbols.Add(symbol);
                        }
                    }
                }

                foreach (var symbol in symbols)
                {
                    List<DataStruct> M = Jump(I[currentIndex], symbol);
                    int foundIndex = I.FindIndex(z => z.SequenceEqual(M));

                    if (foundIndex == -1)
                    {
                        foundIndex = I.Count;
                        I.Add(M);
                        C.Add(M);
                    }

                    f.Add(new Colection(currentIndex, symbol, foundIndex));
                }

                currentIndex++;
            }

            return C;
        }

        private void LoadTables(string n, string t, string filePath)
        {
            int i = 1;
            foreach (var item in Productions)
            {
                P1.Add(new Colection(item.key, item.value, i++));
            }
            Neterminals = n;
            string aux = Neterminals;
            Terminals = t;
            i = 0;
            List<DataStruct> I = Collections[i++];
            t += "$";
            tabelaActiuniSiSalt.Columns.Add("Num", typeof(int));
            DataTable terminals = new();
            foreach (var item in t + n)
            {
                tabelaActiuniSiSalt.Columns.Add(item.ToString(), typeof(string));
            }

            int? nr_rows = (int?)f.Max(z => z.resultList);

            for (int index = 0; index <= nr_rows; index++)
            {
                tabelaActiuniSiSalt.Rows.Add(index);
            }

            foreach (var item in f)
            {
                if (t.Contains((string)item.character))
                {
                    tabelaActiuniSiSalt.Rows[(int)item.listNumber][(string)item.character] = "d" + item.resultList;
                }
                if (n.Contains((string)item.character))
                {
                    tabelaActiuniSiSalt.Rows[(int)item.listNumber][(string)item.character] = item.resultList;
                }
            }
            Reduceri(Collections);
            AfisareInFiser(filePath);
        }

        private void AfisareInFiser(string filepath)
        {
            File.WriteAllText(filepath, String.Empty);
            using (StreamWriter writetext = new(filepath))
            {
                foreach (DataRow row in tabelaActiuniSiSalt.Rows)
                {
                    foreach (DataColumn column in tabelaActiuniSiSalt.Columns)
                    {
                        if (column.ToString() != "Num")
                        {
                            if (Neterminals.Contains(column.ToString()))
                            {
                                if (column == tabelaActiuniSiSalt.Columns[tabelaActiuniSiSalt.Columns.Count - 1])
                                {
                                    writetext.Write(row[column].ToString() != "" ? row[column].ToString() : "0");
                                }
                                else
                                    writetext.Write(row[column].ToString() != "" ? row[column].ToString() + " " : "0 ");
                            }
                            else
                                writetext.Write(row[column].ToString() != "" ? row[column].ToString() + " " : "00 ");
                        }
                    }
                    writetext.WriteLine();
                }
                writetext.Close();
            }
        }

        private void Reduceri(List<List<DataStruct>> c)
        {
            int status = 0;
            int nr = -1;
            foreach (var item in c)
            {
                foreach (var temp in item)
                {
                    if (temp.value.IndexOf(".") == temp.value.Length - 1)
                    {
                        int poz = temp.value.IndexOf(".");
                        Colection? aux = P1.Find(z => z.character.ToString() == temp.value[0..poz]);
                        if (aux != null)
                        {
                            nr = (int)aux.resultList;
                            string where = Urmatorul(temp.key);
                            foreach (var item3 in where)
                            {
                                tabelaActiuniSiSalt.Rows[status][item3.ToString()] = "r" + nr;
                            }
                        }
                        else
                        {
                            if (temp.key == "S")
                            {
                                tabelaActiuniSiSalt.Rows[status]["$"] = "ac";
                            }
                            else
                            {
                                tabelaActiuniSiSalt.Rows[status]["$"] = "error";
                            }
                        }
                    }
                }
                status++;
            }
        }
        private string Urmatorul(string s)
        {
            List<DataStruct> d = Productions.FindAll(z => z.value.Contains(s));
            string aux = "";
            foreach (var item in d)
            {
                int poz = item.value.IndexOf(s);
                if (poz < item.value.Length - 1)
                {
                    aux += item.value.Substring(poz + 1, 1);
                }
                else
                {
                    aux += Urmatorul(item.key);
                }
            }
            aux += "$";
            aux = string.Join("", aux.ToCharArray().Distinct());
            return aux;
        }

        public void LoadData(string filePath)
        {
            LoadTables(Neterminals, Terminals, filePath);
        }
    }
}
