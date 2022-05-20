using Microsoft.Win32;
using PharmacyManagementApp.Context;
using PharmacyManagementApp.Models;
using PharmacyManagementApp.Models.Data;
using PharmacyManagementApp.Services;
using PharmacyManagementApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PharmacyManagementApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            GetAllMedicaments();
        }

        ObservableCollection<Medicament> _medicaments;

        private RelayCommand _loadData;
        public RelayCommand LoadData
        {
            get
            {
                return _loadData ?? new RelayCommand(obj =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        var meds = LoaderService.LoadXml(openFileDialog.FileName);
                        Medicaments = new ObservableCollection<Medicament>(meds);
                        AddOrEditMedicaments(meds);
                        GetAllMedicaments();
                    }

                }
                );
            }
        }
        private RelayCommand _saveData;
        public RelayCommand SaveData
        {
            get
            {
                return _saveData ?? new RelayCommand(obj =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        LoaderService.ExportXml(Medicaments.ToList(), saveFileDialog.FileName);
                    }

                }
                );
            }
        }
        private RelayCommand _editRecord;
        public RelayCommand EditRecord
        {
            get
            {
                return _editRecord ?? new RelayCommand(obj =>
                 {
                     if (!CheckSelectRow())
                         return;
                     EditRecordWindow editRecordWindow = new EditRecordWindow(SelectedMedicament);
                     if (editRecordWindow.ShowDialog() == true)
                     {
                         using (ApplicationDbContext dbContext = new ApplicationDbContext())
                         {
                             var oldMedicament = dbContext.Medicaments.SingleOrDefault(m => m.Id == SelectedMedicament.Id);
                             if (oldMedicament.Price == editRecordWindow.Medicament.Price && oldMedicament.Count == editRecordWindow.Medicament.Count)
                                 return;
                             var change = AddChange(oldMedicament, editRecordWindow.Medicament, false);
                             dbContext.Changes.Add(change);
                             oldMedicament.Price = editRecordWindow.Medicament.Price;
                             oldMedicament.Count = editRecordWindow.Medicament.Count;
                             dbContext.SaveChanges();
                         }
                         GetAllMedicaments();
                     }
                 }
                );
            }
        }
        private RelayCommand _viewRecordChanges;
        public RelayCommand ViewRecordChanges
        {
            get
            {
                return _viewRecordChanges ?? new RelayCommand(obj =>
                {
                    if (!CheckSelectRow())
                        return;
                    var changes = GetChanges(SelectedMedicament.Id);
                    MedicamentChanges changeDialog = new MedicamentChanges(changes);
                    changeDialog.ShowDialog();
                }
                );
            }
        }
        private bool CheckSelectRow()
        {
            if (SelectedMedicament == null)
            {
                MessageBox.Show("Пожалуйста выберите строку!");
                return false;
            }
            return true;
        }

        private Medicament _selectedMedicament;
        public Medicament SelectedMedicament
        {
            get { return _selectedMedicament; }
            set { _selectedMedicament = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Medicament> Medicaments
        {
            get { return _medicaments; }
            set { _medicaments = value; OnPropertyChanged(); }
        }
        public void GetAllMedicaments()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                Medicaments = new ObservableCollection<Medicament>(dbContext.Medicaments.ToList());
            }
        }
        public List<Change> GetChanges(int medicamentId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.Changes.Where(c=>c.MedicamentId==medicamentId).Include(q => q.Medicament).ToList();
            }
        }
        public void AddOrEditMedicaments(List<Medicament> newMedicaments)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var oldMedicaments = dbContext.Medicaments.ToList();
                foreach (var medicament in oldMedicaments)
                {
                    AddOrEdtitSingleMedicament(medicament, newMedicaments, dbContext);
                }
                dbContext.Medicaments.AddRange(newMedicaments.Where(m => !m.IsExist));
                dbContext.SaveChanges();
            }

        }
        public void AddOrEdtitSingleMedicament(Medicament oldMedicament, List<Medicament> newMedicaments, ApplicationDbContext dbContext)
        {
            if (oldMedicament != null)
            {
                var newMedicament = newMedicaments.SingleOrDefault(m => m.MNN == oldMedicament.MNN && m.Name == oldMedicament.Name);
                if (newMedicament == null)
                {
                    oldMedicament.Count = 0;
                }
                else
                {
                    newMedicament.IsExist = true;
                    if (oldMedicament.Price == newMedicament.Price && oldMedicament.Count == newMedicament.Count)
                        return;
                    var change = AddChange(oldMedicament, newMedicament, true);
                    oldMedicament.Price = newMedicament.Price;
                    oldMedicament.Count = newMedicament.Count;
                    oldMedicament.IsExist = newMedicament.IsExist;
                    dbContext.Changes.Add(change);
                }

            }
        }

        private static Change AddChange(Medicament oldMedicament, Medicament newMedicament, bool isAutomatic)
        {
            Change change = new Change();
            change.OldPrice = oldMedicament.Price;
            change.NewPrice = newMedicament.Price;
            change.OldCount = oldMedicament.Count;
            change.NewCount = newMedicament.Count;
            change.IsAutomaticChange = isAutomatic;
            change.MedicamentId = oldMedicament.Id;
            change.Date = DateTime.Now;
            return change;
        }

        public bool DeleteMedicament(Medicament medicament)
        {
            if (medicament != null)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var med = dbContext.Medicaments.SingleOrDefault(m => m.MNN == medicament.MNN && m.Name == medicament.Name);
                    if (med == null)
                        return false;
                    else
                    {
                        dbContext.Medicaments.Remove(med);
                        dbContext.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
