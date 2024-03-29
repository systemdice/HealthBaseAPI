﻿using System.Threading.Tasks;
using MongoDB.GenericRepository.Interfaces;

namespace MongoDB.GenericRepository.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;

        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            try
            {
                //var changeAmount1 = await _context.SaveChanges();
            }
            catch (System.Exception ex)
            {

                
            }
            var changeAmount = await _context.SaveChanges();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
