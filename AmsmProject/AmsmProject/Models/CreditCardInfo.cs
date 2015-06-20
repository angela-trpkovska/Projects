using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmsmProject.Models
{
    public class CreditCardInfo
    {

        [Key]
        public int ID { get; set; }

        public string EMBG { get; set; }

        [Required(ErrorMessage = "Внесете име")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Внесете презиме")]
        public string lastName { get; set; }


       [RegularExpression("^[0-9]{16}$", ErrorMessage = "Невалиден број за  картичка")]
        [Required(ErrorMessage = "Внесете број на картичка")]

        public string cardNumber { get; set; }

        [Required(ErrorMessage = "Внесете датум на важење")]
        public string expiresDate { get; set; }



       [RegularExpression("^[0-9]{3}$", ErrorMessage = "Невалиден код")]
       [Required(ErrorMessage = "Внесете го три цифрениот код")]
        public int code { get; set; }
    }
}