using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace demandModul.Models
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        [Display(Name = "Comment Text")]
        [Required, StringLength(500)]
        public string CommentText { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreateTime { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Suggestion Suggestion { get; set; }
        
    }
}