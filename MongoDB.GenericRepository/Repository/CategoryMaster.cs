using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class CategoryMasterRepository : BaseRepository<CategoryMaster>, ICategoryMasterRepository
    {
        public CategoryMasterRepository(IMongoContext context) : base(context)
        {
        }
    }
}
