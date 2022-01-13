using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class BedManagementRepository : BaseRepository<BedManagement>, IBedManagementRepository
    {
        public BedManagementRepository(IMongoContext context) : base(context)
        {
        }
    }
}
