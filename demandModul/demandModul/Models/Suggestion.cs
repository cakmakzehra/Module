using demandModul.Models.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace demandModul.Models
{
    public class Suggestion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuggestionID { get; set; }

        public string Name { get; set; }

        [Display(Name = "Suggestion Type")]
        public virtual SuggestionType SuggestionType { get; set; }

        [ForeignKey("SuggestionType")]
        public int? SuggestionTypeID { get; set; }

        [Required,StringLength(500)]
        public string Explanation { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Last Update Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? lastUpdateDate { get; set; }

        [Display(Name = "Approved Status")]
        public string ApprovedStatus { get; set; }

        [Display(Name = "Approved Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovedDate { get; set; }

        [Display(Name = "Denied Note"), StringLength(500)]
        public string DeniedNote { get; set; }

        [Display(Name = "Approver Employee")]
        public string AppEmployee { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Like> Likes { get; set; }

        public int LikesCount { get; set; }

    }
}
