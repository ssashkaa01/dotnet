using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebTestSecirity.Models;

namespace WebTestSecirity.Entities
{
    [Table("tblUserProfiles")]
    public class UserProfile
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }

        [Required, StringLength(255)]
        public string LastName { get; set; }

        [Required, StringLength(255)]
        public string FirstName { get; set; }

        [Required, StringLength(255)]
        public string Patronymic { get; set; }

        public Gender Gender { get; set; }

        [StringLength(4000)]
        public string Hobby { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}