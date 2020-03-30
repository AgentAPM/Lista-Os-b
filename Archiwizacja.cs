using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Osoby
{
    class Archiwizacja
    {
        public static void ZapiszDoPliku(string plik, string[] obiekty)
        {
            using (StreamWriter fstream = new StreamWriter(plik))
            {
                foreach (string s in obiekty)
                    fstream.WriteLine(s);
                fstream.Close();
            }
        }
        public static string[] OdczytajZPliku(string plik)
        {
            string[] obiekty = null;
            if (File.Exists(plik))
            {
                obiekty = File.ReadAllLines(plik);
            }
            if (obiekty == null)
            {
                return new string[0];
            } else {
                return obiekty;
            }
        }
    }
}
