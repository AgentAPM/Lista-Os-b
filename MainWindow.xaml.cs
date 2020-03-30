using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Osoby
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string archiwum = "archiwum.txt";
        #region Konstruktory
        public MainWindow()
        {
            InitializeComponent();
            ClearForm();
            
        }
        #endregion
        #region Metody
        private void LoadPlayer(Pilkarz toLoad)
        {
            Imie.Text = toLoad.Imie;
            Nazwisko.Text = toLoad.Nazwisko;
            Wiek.Value = toLoad.Wiek;
            Waga.Value = toLoad.Waga;
            Zmien.IsEnabled = true;
            Usun.IsEnabled = true;
        }

        private void ClearForm()
        {
            Imie.Text = "";
            Nazwisko.Text = "";
            Wiek.Value = 28;
            Waga.Value = 75;

            Zmien.IsEnabled = false;
            Usun.IsEnabled = false;
            ListaPilkarzy.SelectedIndex = -1;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (Imie.IsNonEmpty() & Nazwisko.IsNonEmpty())
            {
                var nowyPilkarz = new Pilkarz(Imie.Text.ToString(), Nazwisko.Text.ToString(), (uint)Wiek.Value, (uint)Waga.Value);
                bool alreadyExists = false;
                foreach(Pilkarz p in ListaPilkarzy.Items)
                {
                    if (p.CompareTo(nowyPilkarz))
                    {
                        alreadyExists = true;
                        break;
                    }
                }
                if (alreadyExists)
                {
                    var dialog = MessageBox.Show($"{nowyPilkarz.ToString()} już jest na liście {Environment.NewLine} Czy wyczyścić formularz?", "Uwaga", MessageBoxButton.OKCancel);
                    if (dialog == MessageBoxResult.OK)
                    {
                        ClearForm();
                    }
                } else {
                    ListaPilkarzy.Items.Add(nowyPilkarz);
                    ClearForm();
                }
            }
            
        }
        private void Zmien_Click(object sender, RoutedEventArgs e)
        {
            var nowyPilkarz = new Pilkarz(Imie.Text.ToString(), Nazwisko.Text.ToString(), (uint)Wiek.Value, (uint)Waga.Value);
            bool alreadyExists = false;
            foreach (Pilkarz p in ListaPilkarzy.Items)
            {
                if (p.CompareTo(nowyPilkarz))
                {
                    alreadyExists = true;
                    break;
                }
            }
            if (alreadyExists)
            {
                var dialog = MessageBox.Show($"{nowyPilkarz.ToString()} już jest na liście {Environment.NewLine}Dodać mimo to?", "Uwaga", MessageBoxButton.YesNo);
                if (dialog == MessageBoxResult.OK)
                {
                    ListaPilkarzy.Items.Add(nowyPilkarz);
                    ClearForm();
                }
            }
            else
            {
                var dialog = MessageBox.Show($"{Environment.NewLine}{ListaPilkarzy.SelectedItem.ToString()}{Environment.NewLine}Zamienić na:{Environment.NewLine}{nowyPilkarz.ToString()}", "Uwaga", MessageBoxButton.YesNo);
                if (dialog == MessageBoxResult.Yes)
                {
                    ListaPilkarzy.Items[ListaPilkarzy.SelectedIndex] = nowyPilkarz;
                    ClearForm();
                }
            }
        }
        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            Pilkarz doUsuniecia = (Pilkarz)ListaPilkarzy.SelectedItem;
            var dialog = MessageBox.Show($"Usunąć: {doUsuniecia.ToString()}?", "Uwaga", MessageBoxButton.YesNo);
            if (dialog == MessageBoxResult.Yes)
            {
                ListaPilkarzy.Items.RemoveAt(ListaPilkarzy.SelectedIndex);
                ClearForm();
            }
            
        }
        private void ListaPilkarzy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaPilkarzy.SelectedIndex > -1) { 
                LoadPlayer((Pilkarz)ListaPilkarzy.SelectedItem);
                }
        }

        #endregion

        private void Ready(object sender, RoutedEventArgs e)
        {
            string[] pilkarze = Archiwizacja.OdczytajZPliku(archiwum);
            Pilkarz wczytany;
            foreach (string p in pilkarze)
            {
                try
                {
                    wczytany = Pilkarz.FromString(p);
                } catch(Exception error) { continue; }
                ListaPilkarzy.Items.Add(wczytany);
            }
            
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int n = ListaPilkarzy.Items.Count;
            string[] pilkarze = new string[n];
            for(int i = 0; i < n; i++)
            {
                pilkarze[i] = (ListaPilkarzy.Items[i] as Pilkarz).ToFileString();
            }
            Archiwizacja.ZapiszDoPliku(archiwum, pilkarze);
            
        }
    }
}
