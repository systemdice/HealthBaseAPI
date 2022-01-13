
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class PrintBillRepository : BaseRepository<PrintBill>, IPrintBillRepository
    {
        public PrintBillRepository(IMongoContext context) : base(context)
        {
        }
    }
}

