using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class ReportViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;

        public string DateStart { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public PieChartModel PieChartModel { get; set; }
        public BarChartModel BarChartModel { get; set; }
    }
}
