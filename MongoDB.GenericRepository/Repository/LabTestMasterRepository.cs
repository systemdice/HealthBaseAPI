using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class LabTestMasterRepository : BaseRepository<LabTestMaster>, ILabTestMasterRepository
    {
        public LabTestMasterRepository(IMongoContext context) : base(context)
        {
        }
    }
}
