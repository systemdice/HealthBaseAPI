using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class FollowupRepository : BaseRepository<Followup>, IFollowupRepository
    {
        public FollowupRepository(IMongoContext context) : base(context)
        {
        }
    }
}
