using PharmacyManagementApp.Models;
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
using System.Windows.Shapes;
using PharmacyManagementApp.Models.Data;
namespace PharmacyManagementApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EditRecordWindow.xaml
    /// </summary>
    public partial class EditRecordWindow : Window
    {
        public Medicament Medicament { get; set; }
        public EditRecordWindow(Medicament selectedMedicament)
        {
            InitializeComponent();
            Medicament = selectedMedicament;
            textMNN.Text = Medicament.MNN;
            textName.Text = Medicament.Name;
            textPrice.Text = Medicament.Price.ToString();
            textCount.Text = Medicament.Count.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double price = 0;
            int count = 0;
            if (!double.TryParse(textPrice.Text, out price))
            {
                MessageBox.Show("Цена должна быть числом!");
                return;
            }
            if (!int.TryParse(textCount.Text, out count))
            {
                MessageBox.Show("Количество должно быть числом!");
                return;
            }
            Medicament.Price = price;
            Medicament.Count = count;
            DialogResult = true;
        }
    }
}
