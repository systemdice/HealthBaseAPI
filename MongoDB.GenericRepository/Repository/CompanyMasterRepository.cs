using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class CompanyMasterRepository : BaseRepository<CompanyMaster>, ICompanyMasterRepository
    {
        public CompanyMasterRepository(IMongoContext context) : base(context)
        {
        }
    }
}
