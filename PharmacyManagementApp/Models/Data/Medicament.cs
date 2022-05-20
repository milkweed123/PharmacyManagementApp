﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementApp.Models.Data
{
    public class Medicament
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MNN { get; set; }
        [Required]
        public string Name { get; set; }

        public double Price { get; set; }

        public int Count { get; set; }
       
        [NotMapped]
        public bool IsExist { get; set; }
    }
}
