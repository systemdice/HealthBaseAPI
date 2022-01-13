using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.ViewModel
{
    public class TestsCategoryViewModel
    {
	public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
        public string Name { get; set; }
        public string ShortName{ get; set; }
        public string Category{ get; set; }
        public decimal Fee{ get; set; }
        public string Method{ get; set; }
        public string Instrument{ get; set; }
        public bool ResultTypeDoc{ get; set; }

        public string Notes { get; set; }
        public string Comments{ get; set; }
        public string Interpretation{ get; set; }

        public Parameters[] Parameters{ get; set; }
        public ReferenceRange[] ReferenceRange { get; set; }

    }
}
