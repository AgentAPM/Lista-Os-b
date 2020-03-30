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
    /// Logika interakcji dla klasy TextBoxtWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxtWithErrorProvider : UserControl
    {
        #region Własności
        public string Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }
        public Brush TextBoxBrush
        {
            get
            {
                return (border.BorderBrush);
            }
            set
            {
                border.BorderBrush = value;
            }
        }
        #endregion
        #region Konstruktory
        public TextBoxtWithErrorProvider()
        {
            InitializeComponent();
        }
        #endregion
        #region Metody
        public void SetPopup(string message)
        {
            popTipText.Text = message;
            if(message=="")
            {
                border.BorderThickness = new Thickness(0);
                popupTip.Visibility = Visibility.Hidden;
            } else {
                border.BorderThickness = new Thickness(1);
                popupTip.Visibility = Visibility.Visible;
            }
        }
        public bool IsNonEmpty()
        {
            textBox.Text = textBox.Text.Trim();
            if (textBox.Text == "")
            {
                SetPopup("Pole nie może być puste");
                return false;
            } else {
                SetPopup("");
                return true;
            }

        }
        #endregion

    }
}
