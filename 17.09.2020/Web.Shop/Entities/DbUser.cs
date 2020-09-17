using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Shop.Entities
{
    public class DbUser : IdentityUser<long>
    {
        public virtual ICollection<DbUserRole> UserRoles { get; set; }

        [Required, StringLength(255)]
        public string Image { get; set; }
    }
}
