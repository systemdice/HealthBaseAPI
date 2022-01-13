using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class PatientDetailsRepository : BaseRepository<PatientDetails>, IPatientDetailsRepository
    {
        public PatientDetailsRepository(IMongoContext context) : base(context)
        {
        }
    }
}
