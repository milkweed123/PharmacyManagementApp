using PharmacyManagementApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PharmacyManagementApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MedicametChanges.xaml
    /// </summary>
    public partial class MedicamentChanges : Window
    {
        ObservableCollection<Change> Changes { get; set; }
        public MedicamentChanges(List <Change> changes)
        {
            InitializeComponent();
            Changes = new ObservableCollection<Change>(changes);
            ViewChanges.ItemsSource = Changes;
        }
    }
}
