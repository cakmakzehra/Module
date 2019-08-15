using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace demandModul.Models
{
    public class Like
    {
        public int LikeID { get; set; }

        public DateTime? LikedDate { get; set; }

        public bool Liked { get; set; }

        public virtual Suggestion Suggestion { get; set; }

        [ForeignKey("Suggestion")]
        public int SuggestionID { get; set; }

        public virtual Employee Employee { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
    
    }
}