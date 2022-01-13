using System;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class LabTestIndividualRepository : BaseRepository<LabTestIndividual>, ILabTestIndividualRepository
    {
        public LabTestIndividualRepository(IMongoContext context) : base(context)
        {
        }
    }
}
