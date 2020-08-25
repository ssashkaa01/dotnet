using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebTestSecirity.Entities
{
    public enum Gender
    {
        [Display(Name = "Чоловік")]
        Male = 0,
        [Display(Name = "Жінка")]
        Female = 1
    }
}