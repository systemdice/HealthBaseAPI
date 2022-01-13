using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class TimesheetRepository : BaseRepository<Timesheet>, ITimesheetRepository
    {
        public TimesheetRepository(IMongoContext context) : base(context)
        {
        }
    }
}
