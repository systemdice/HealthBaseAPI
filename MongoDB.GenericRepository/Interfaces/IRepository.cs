using MongoDB.GenericRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        Task<TEntity> GetById(string UnqueID);
        Task<TEntity> GetByOPIPId(string UnqueID);
        Task<TEntity> GetByIdOnly(string UnqueID);
        Task<TEntity> GetByIdDateStartOnly(string DateStart);
        Task<TEntity> Getanything(string paramVal);
        Task<TEntity> GetByName(string UnqueID,string filterColumnName, string finTheUser);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetLoginDetails(string UName,string pwd);
        Task<IEnumerable<TEntity>> GetAllCase();
        Task<IEnumerable<NewModifyCase>> GetAllCase1(string noofdaysDate, string OneorMany, string[] dtFilterRange);
        Task<IEnumerable<Expenses>> GetAllExpenses1(string noofdaysDate, string OneorMany, string[] dtFilterRange);
        Task<IEnumerable<TEntity>> GetStaffType(string StaffType);
        Task<IEnumerable<TEntity>> GetAllCategoryWithType(string categoryType);
        Task<IEnumerable<TEntity>> getUniquePatient(string username, string contactNumber);
        Task<IEnumerable<TEntity>> GetAllPaymentHistory(string caseID);
        Task<IEnumerable<TEntity>> GetAllPharmaFilter(string entryowner, string caseID);
        void Update(TEntity obj, string Update);
        void Remove(string UnqueID);
        void RemoveBasedOnCASEID(string caseID);
        long GetCount(TEntity obj,string type1,string type2);
        long GetOPIPVSCount(TEntity obj, string opdipdType);
    }
}
