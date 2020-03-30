using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osoby
{
    internal class Pilkarz
    {
        #region Własnosci
        public string Imie { get; set; } = "";
        public string Nazwisko { get; set; }="";
        public uint Wiek { get; set; }
        public uint Waga { get; set; }
        #endregion
        #region Konstruktory
        public Pilkarz(string imie, string nazwisko, uint wiek, uint waga)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
        }
        public Pilkarz(string imie, string nazwisko) : this(imie,nazwisko,28,75){}
        #endregion
        #region Metody
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} lat {Wiek}, waga {Waga}kg";
        }
        public string ToFileString()
        {
            return $"{Imie}|{Nazwisko}|{Wiek}|{Waga}";
        }
        public static Pilkarz FromString(string tekst)
        {
            string[] dane = tekst.Split('|');
            if (dane.Length == 4)
            {
                return new Pilkarz(
                    dane[0].Trim(), 
                    dane[1].Trim(), 
                    uint.Parse(dane[2]), 
                    uint.Parse(dane[3])
                    );
            }
            else throw new Exception("Błędny format");
        }

        public bool CompareTo(Pilkarz pilkarz)
        {
            if (pilkarz.Nazwisko != Nazwisko) return false;
            if (pilkarz.Imie != Imie) return false;
            if (pilkarz.Wiek != Wiek) return false;
            if (pilkarz.Waga != Waga) return false;
            return true;
        }
        #endregion
    }
}
