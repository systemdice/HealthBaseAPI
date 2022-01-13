using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class AddQuestionRepository : BaseRepository<AddQuestion>, IAddQuestionRepository
    {
        public AddQuestionRepository(IMongoContext context) : base(context)
        {
        }
    }
}
