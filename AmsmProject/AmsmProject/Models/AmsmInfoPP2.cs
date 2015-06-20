using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmsmProject.Models
{
    public class AmsmInfoPP2
    {

        [Key]
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        public int hour { get; set; }

        public string place { get; set; }

        public int candidatesPP2 { get; set; }
    }
}