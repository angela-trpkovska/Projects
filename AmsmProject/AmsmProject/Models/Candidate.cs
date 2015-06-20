using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmsmProject.Models
{
    public class Candidate
    {
        [Key]
        [RegularExpression("^[0-9]{13}$", ErrorMessage = "Матичниот број има невалиден формат")]
        [Required(ErrorMessage = "Внесете го вашиот матичен број")]
        public string EMBG { get; set; }



        [StringLength(20, MinimumLength = 3, ErrorMessage = "Името е невалидно")]
        [Required(ErrorMessage = "Внесете го вашето име")]
        public string firstName { get; set; }




        [StringLength(20, MinimumLength = 3, ErrorMessage = "Татковото име е невалидно")]
        [Required(ErrorMessage = "Внесете татково име")]
        public string parentName { get; set; }




        [StringLength(30, MinimumLength = 3, ErrorMessage = "Презимето е невалидно")]
        [Required(ErrorMessage = "Внесете го вашето презиме")]
        public string lastName { get; set; }



        [Required(ErrorMessage = "Изберете категорија")]
        public string category { get; set; }




        [StringLength(30, MinimumLength = 3, ErrorMessage = "Името на авто школата е невалидно")]
        [Required(ErrorMessage = "Внесете го името на авто школа")]
        public string drivingSchool { get; set; }


        [StringLength(30, MinimumLength = 3, ErrorMessage = "Името на инструкторот е невалидно")]
        [Required(ErrorMessage = "Внесете го името на  инструкторот")]
        public string instructor { get; set; }

        public bool registered { get; set; }

       
        
    }
}