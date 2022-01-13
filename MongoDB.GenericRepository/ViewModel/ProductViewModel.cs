namespace MongoDB.GenericRepository.ViewModel
{
    public class ProductViewModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string UnqueID { get; set; }
        public bool ShouldCommit { get; set; } = true;
    }
}
