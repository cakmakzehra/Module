using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace demandModul.Models
{
    public class Location
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }

        [StringLength(200), Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [StringLength(500)]
        public string Addresss { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }

        public string Status { get; set; }

        [Display(Name = "Create Employee")]
        public virtual Employee CreateEmployee { get; set; }

    }
}