using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmsmProject.Models
{
    public class TheoryPart
    {
        [Key]
        public int ID { get; set; }

        public string EMBG { get; set; }

        //datum koga e zakazano za polaganje
        [DataType(DataType.Date)]
       // [DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Изберете датум")]
        public DateTime date { get; set; }

        public int hour { get; set; }

        //otkako ke zakaze termin passed=false,a ako polozi potoa se updejtira na true

        public bool passed { get; set; }

        //dali platil ili ne
        public bool payed { get; set; }

       // public virtual Candidate Candidate { get; set; }
    }
}