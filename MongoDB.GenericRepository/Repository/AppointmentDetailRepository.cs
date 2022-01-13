using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class AppointmentDetailRepository : BaseRepository<AppointmentDetail>, IAppointmentDetailRepository
    {
        public AppointmentDetailRepository(IMongoContext context) : base(context)
        {
        }
    }
}
