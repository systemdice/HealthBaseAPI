
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class LeaveMangementRepository : BaseRepository<LeaveMangement>, ILeaveMangementRepository
    {
        public LeaveMangementRepository(IMongoContext context) : base(context)
        {
        }
    }
}
