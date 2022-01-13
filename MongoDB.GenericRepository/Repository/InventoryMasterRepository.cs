
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class InventoryMasterRepository : BaseRepository<InventoryMaster>, IInventoryMasterRepository
    {
        public InventoryMasterRepository(IMongoContext context) : base(context)
        {
        }
    }
}

