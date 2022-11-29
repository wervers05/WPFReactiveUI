using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFReactiveUI.Model
{
    internal class ExcelModel : ReactiveObject
    {
        public string OrderDate { get; set; }

        public string Region { get; set; }

        public string Rep { get; set; }

        public string Item { get; set; }

        public int Units { get; set; }

        public double UnitCost { get; set; }

        public double Total { get; set; }
    }
}
