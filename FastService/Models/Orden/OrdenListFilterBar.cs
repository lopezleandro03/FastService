using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastService.Models.Orden
{
    public class OrdenListFilterBar
    {
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public int ToMonth { get; set; }
        public int ToYear { get; set; }
        public int? MinInactiveDays { get; set; }
        public List<int> SelectedEstados { get; set; }

        public MultiSelectList EstadosList { get; set; }
        public SelectList MonthList { get; set; }
        public SelectList YearList { get; set; }
    }
}