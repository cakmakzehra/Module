using demandModul.Models.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace demandModul.Models
{
    public class Fault
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FaultID { get; set; }

        [Required, StringLength(500)]
        public string Explanation { get; set; }

        [Display(Name = "Fault Type")]
        public virtual FaultType FaultType { get; set; }

        [ForeignKey("FaultType")]
        public int FaultTypeID { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public virtual Location Location { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }

        [Display(Name = "Related Department")]
        public string RelatedDepartment{get;set;}

        [Display(Name = "Intended Maintenance Time")]
        public string IntendedMaintenanceTime { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Fault Status")]
        public string FaultStatus { get; set; }

        [Display(Name = "Applied Maintenance Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppliedMaintenanceDate { get; set; }

        [Display(Name = "Maintenance Explanation"),StringLength(300)]
        public string MaintenanceExplanation { get; set; }

        [Display(Name = "Maintenance Employee")]
        public string MaintenanceEmployee { get; set; }

        public virtual Employee Employee { get; set; }
    }
}