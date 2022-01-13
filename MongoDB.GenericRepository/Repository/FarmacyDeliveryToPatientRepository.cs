using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class FarmacyDeliveryToPatientRepository : BaseRepository<FarmacyDeliveryToPatient>, IFarmacyDeliveryToPatientRepository
    {
        public FarmacyDeliveryToPatientRepository(IMongoContext context) : base(context)
        {
        }
    }
}

