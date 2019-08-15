using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demandModul.Models
{
    [Table("Demand")]
    public class Demand
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DemandID { get; set; }

        [StringLength(200), Required(ErrorMessage = "Name is required")]
        [Display(Name = "Inventory Name")]
        public string InventoryName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Unit Type")]
        public virtual UnitType UnitType { get; set; }

        [ForeignKey("UnitType")]
        public int UnitTypeID { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }

        [Required]
        public string Urgency { get; set; }

        [Display(Name = "Approved Status")]
        public string ApprovedStatus { get; set; }

        [Display(Name = "Approver Employee")]
        public string ApproverEmployee { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppDate { get; set; }

        [Display(Name = "Approved Note"), StringLength(500)]
        public string AppNote { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public virtual Location Location { get; set; }

        [ForeignKey("Location")]
        public int LocationID { get; set; }

        [StringLength(500),Required(ErrorMessage = "Explanation is required")]
        public string Explanation { get; set; }

        public virtual Employee Employee { get; set; }
    }
}