using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class NewModifyCaseRepository : BaseRepository<NewModifyCase>, INewModifyCaseRepository
    {
        public NewModifyCaseRepository(IMongoContext context) : base(context)
        {
        }
    }
}
