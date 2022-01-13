using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class UnitsCategoryRepository : BaseRepository<UnitsCategory>, IUnitsCategoryRepository
    {
        public UnitsCategoryRepository(IMongoContext context) : base(context)
        {
        }
    }
}
