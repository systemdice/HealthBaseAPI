using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class VaccinationsRepository : BaseRepository<Vaccinations>, IVaccinationsRepository
    {
        public VaccinationsRepository(IMongoContext context) : base(context)
        {
        }
    }
}
