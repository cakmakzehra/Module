using demandModul.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demandModul.ViewModels.SuggestionViewModel
{
    public class SuggestionLike
    {
        public Suggestion Suggestion { get; set; }
        public List<Like> Like { get; set; }
    }
}