using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class AvailabilityViewModel
    {
        public string UnqueID { get; set; }

        public string Name { get; set; }
        public Timeslot[] PesronAvailability { get; set; }
    }
}
