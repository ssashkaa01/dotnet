using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Shop.Models
{
    public class UserVM
    {
        public long Id { get; set; }
        public string Email { get; set; }
    }
}
