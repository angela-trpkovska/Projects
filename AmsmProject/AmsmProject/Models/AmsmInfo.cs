using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmsmProject.Models
{
    public class AmsmInfo
    {
        [Key]

        public int ID { get; set; }

        [DataType(DataType.Date)]
      //  [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        //[DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        public int hour { get; set; }

        public int  candidatesTP { get; set; }

        public int candidatesPP1 { get; set; }

        


    }
}