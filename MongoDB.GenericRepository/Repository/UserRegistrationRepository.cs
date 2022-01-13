using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Repository
{
    public class UserRegistrationRepository: BaseRepository<UserRegistration>, IUserRegistrationRepository
    {
        public UserRegistrationRepository(IMongoContext context) : base(context)
        {
        }
    }
}
