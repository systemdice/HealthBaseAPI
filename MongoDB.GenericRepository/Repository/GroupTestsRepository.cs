

using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class GroupTestsRepository : BaseRepository<GroupTests>, IGroupTestsRepository
    {
        public GroupTestsRepository(IMongoContext context) : base(context)
        {
        }
    }
}
