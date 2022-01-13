using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class ReferralMasterRepository : BaseRepository<ReferralMaster>, IReferralMasterRepository
    {
        public ReferralMasterRepository(IMongoContext context) : base(context)
        {
        }
    }
}
