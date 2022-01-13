using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Repository
{
    public class DeptInvestigationRepository: BaseRepository<DeptInvestigation>, IDeptInvestigationRepository
    {
        public DeptInvestigationRepository(IMongoContext context) : base(context)
        {
        }
    }
}
