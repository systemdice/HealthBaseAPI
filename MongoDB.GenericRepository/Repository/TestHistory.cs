using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class TestHistoryRepository : BaseRepository<TestHistory>, ITestHistoryRepository
    {
        public TestHistoryRepository(IMongoContext context) : base(context)
        {
        }
    }
}
