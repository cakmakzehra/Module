using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace demandModul.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        [Display(Name = "Mail Address")]
        [StringLength(100),Required]
        public string MailAddress { get; set; }

        [Display(Name = "Name and Surname"),Required]
        public string NameSurname { get; set; }

        [Required]
        public string Department { get; set; }

        public string Title { get; set; }

        [StringLength(100), Required]
        public string Password { get; set; }

        public virtual List<Demand> Demands { get; set; }

        public virtual List<Fault> Faults { get; set; }

        public virtual List<Suggestion> Suggestions { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Like> Likes { get; set; }

    }
}