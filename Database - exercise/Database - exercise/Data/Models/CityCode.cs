using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database___exercise.Data.Models
{
    public class CityCode
    {
        [Key]
        public int Id { get; set; }

        public int Number { get; set; }
    }
}
