using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementApp.Models.Data
{
    public class Change
    {
        [Key]
        public int Id { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public int OldCount { get; set; }
        public int NewCount { get; set; }

        public bool IsAutomaticChange { get; set; }
        [NotMapped]
        public string AutomaticString
        {
            get
            {
                return IsAutomaticChange ? "Автоматические" : "Ручные";
            }
        }
        public DateTime Date { get; set; }
        public virtual Medicament Medicament { get; set; }
        public int MedicamentId { get; set; }
        [NotMapped]
        public string ChangeString => ToString();
        public override string ToString()
        {
            var str = "";
            if (OldCount != NewCount)
            {
                str += "Количество изменилось с " + OldCount.ToString() + " до " + NewCount.ToString() + ".";
            }
            if (OldPrice != NewPrice)
            {
                str += "Цена изменилась с " + OldPrice.ToString() + " до " + NewPrice.ToString() + ".";
            }
            return str;
        }
    }
}
