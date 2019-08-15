using demandModul.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demandModul.ViewModels.IndexViewModel
{
    public class IndexViewModel
    {
        public List<Suggestion> Suggestion { get; set; }
        public List<Demand> Demand { get; set; }
        public List<Fault> Fault { get; set; }
    }
}