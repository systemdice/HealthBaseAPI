using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class ExpensesRepository : BaseRepository<Expenses>, IExpensesRepository
    {
        public ExpensesRepository(IMongoContext context) : base(context)
        {
        }
    }
}
