using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class PaymentHistoryRepository : BaseRepository<PaymentHistory>, IPaymentHistoryRepository
    {
        public PaymentHistoryRepository(IMongoContext context) : base(context)
        {
        }
    }
}
