using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Model
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Timeslot
    {
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string date { get; set; }

    }

    public class Availability
    {
        public Availability(AvailabilityViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();
            Name = UR.Name;
            PesronAvailability = UR.PesronAvailability;
        }

        public Availability(string updateUniqueaID, AvailabilityViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            Name = UR.Name;
            PesronAvailability = UR.PesronAvailability;
        }
        public Guid Id { get; set; }
        public string UnqueID { get; set; }


        public string Name { get; set; }
        public Timeslot[] PesronAvailability { get; set; }
    }
}
