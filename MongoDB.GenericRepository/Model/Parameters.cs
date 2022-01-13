using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Model
{
    public class Parameters
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string InputType { get; set; }
        public bool Optional { get; set; }
        public bool Removed { get; set; }
    }
}
