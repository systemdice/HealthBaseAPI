using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.ViewModel
{
    public class AddQuestionViewModel
    {
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;

        public string DateStart { get; set; } = new DateTime().ToString("dd-MM-yyyy");
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<Question> questions { get; set; }


    }
}
