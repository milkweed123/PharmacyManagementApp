using PharmacyManagementApp.Models;
using PharmacyManagementApp.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;

namespace PharmacyManagementApp.Services
{
    public class LoaderService
    {
        public static List<Medicament> LoadXml(string path)
        {
            var medicaments = new List<Medicament>();
            try
            {
                var serializer = new XmlSerializer(typeof(EXPORT));
                using (StringReader reader = new StringReader(File.ReadAllText(path)))
                {
                    var export = (EXPORT)serializer.Deserialize(reader);
                    foreach (var item in export.LS)
                    {
                        var medicament = new Medicament { MNN = item.MNN, Name = item.DATA.NAME, Count = (int)item.DATA.COUNT, Price = item.DATA.PRICE };
                        medicaments.Add(medicament);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Формат файла не совпадает с схемой");
                medicaments.Clear();

            }
            return medicaments;
        }
        public static void ExportXml(List<Medicament> medicaments, string path)
        {
            var serializer = new XmlSerializer(typeof(EXPORT));
            var export = new EXPORT();
            export.LS = new List<LS>();
            foreach (var item in medicaments)
            {
                LS ls = new LS() {MNN = item.MNN};
                ls.DATA = new DATA() { PRICE = item.Price, COUNT = item.Count,NAME=item.Name };
                export.LS.Add(ls);
            }
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                serializer.Serialize(streamWriter, export) ;
            }
           
        }
    }
}
