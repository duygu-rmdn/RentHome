using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database___exercise.Data.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Size { get; set; }

        public int CityCodeId { get; set; }

        public CityCode CityCode { get; set; }
    }
}
