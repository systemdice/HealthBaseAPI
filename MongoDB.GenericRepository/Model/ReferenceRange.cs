using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Model
{
    public class ReferenceRange
    {
        public string Gender { get; set; }
        public string MinimumAge { get; set; }
        public string MaximumAge { get; set; }
        public string LowerValue { get; set; }
        public string UpperValue { get; set; }
        public bool Remarks { get; set; }
    }
}
