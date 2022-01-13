using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class TestsCategoryRepository : BaseRepository<TestsCategory>, ITestsCategoryRepository
    {
        public TestsCategoryRepository(IMongoContext context) : base(context)
        {
        }
    }
}
