using MongoDB.Driver;
using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace MongoDB.GenericRepository.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DbSet;
        protected IMongoCollection<NewModifyCase> DbSet1;
        protected IMongoCollection<Expenses> DbSet2;

        protected BaseRepository(IMongoContext context)
        {
            Context = context;
        }

        public virtual void Add(TEntity obj)
        {
            ConfigDbSet();
            Context.AddCommand(() => DbSet.InsertOneAsync(obj));
            //Context.AddCommand(() => DbSet1.InsertOneAsync(new NewModifyCase()));
        }

        private void ConfigDbSet()
        {
            try
            {
                DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
                DbSet1 = Context.GetCollection<NewModifyCase>("NewModifyCase");
                DbSet2 = Context.GetCollection<Expenses>("Expenses");
            }
            catch (Exception ex)
            {

                
            }
            
        }

        public virtual async Task<TEntity> GetById(string UnqueID)
        {
            ConfigDbSet();

            //var items = new List<SelectPropertyForFilter>();
            //get names
            //var names = items.Select(x => x.GetType().GetProperty(UnqueID).GetValue(x));
            //get descriptions
            //var descriptions = items.Select(x => x.Description);
            //var xyz = Context.GetCollection<Product>("Product");
            //var filter = Builders<Product>.Filter.Eq("UnqueID", UnqueID);
            //var result = await xyz.FindAsync(filter);

            //IEnumerable<Product> listOfUsers;
            //var cursor = await xyz.FindAsync(x => x.UnqueID == UnqueID);
            //while (await cursor.MoveNextAsync())
            //{
            //    listOfUsers = cursor.Current;
            //}
            ////return listOfUsers.Current.Single();
            //var user = await xyz.Find(x => x.UnqueID == UnqueID).FirstAsync();
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("UnqueID", UnqueID));
            return data.SingleOrDefault();
            //return user.
        }

        public virtual async Task<TEntity> GetByOPIPId(string UnqueID)
        {
            ConfigDbSet();

            var builder = Builders<TEntity>.Filter;
            //filteroptio
            //var filter = builder.Eq("EntryOwner", entryOwner);
            FilterDefinition<TEntity> filter;
            filter = builder.Eq("IPDOPDId", UnqueID) | builder.Eq("UnqueID", UnqueID);

            var data = await DbSet.FindAsync(filter);
            return data.SingleOrDefault();
            //return user.
        }

        public virtual async Task<IEnumerable<TEntity>> getUniquePatient(string username, string contactNumber)
        {
            ConfigDbSet();
            IAsyncCursor<TEntity> all = null;
            try
            {
                var builder = Builders<TEntity>.Filter;
                //filteroptio
                //var filter = builder.Eq("EntryOwner", entryOwner);
                FilterDefinition<TEntity> filter;
                filter = builder.Eq("FirstName", username) | builder.Eq("ContactNumber", contactNumber);
                //var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("UnqueID", UnqueID));
                all = await DbSet.FindAsync(filter);

                //all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);

            }
            catch (Exception ex)
            {

                //throw;
            }
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual long GetCount(TEntity obj, string type1, string type2)
        {
            ConfigDbSet();

            var builder = Builders<TEntity>.Filter;
            var xyz = DbSet.CountDocuments(builder.Eq("CeditStatus", type1) & builder.Eq("PharmacyStoreName", type2));
            return xyz; // .SingleOrDefault();
            
        }
        public virtual long GetOPIPVSCount(TEntity obj, string opdipdType)
        {
            ConfigDbSet();

            var builder = Builders<TEntity>.Filter;
            var xyz = DbSet.CountDocuments(builder.Eq("OPDkimbaIPD", opdipdType));
            return xyz; // .SingleOrDefault();

        }
        public virtual async Task<IEnumerable<TEntity>> GetAllPharmaFilter(string entryOwner,string caseID)
        {
            ConfigDbSet();
            IAsyncCursor<TEntity> all = null;
            try
            {
                var builder = Builders<TEntity>.Filter;
                //filteroptio
                //var filter = builder.Eq("EntryOwner", entryOwner);
                FilterDefinition<TEntity> filter;
                if (caseID == "")
                {
                    filter = builder.Eq("EntryOwner", entryOwner) | builder.Eq("PharmacyStoreName", entryOwner);
                }
                else if (entryOwner == "")
                {
                    filter = builder.Eq("CaseID", caseID);
                }
                else
                {
                    filter = builder.Eq("PharmacyStoreName", entryOwner) & builder.Eq("CaseID", caseID);
                }
                //var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("UnqueID", UnqueID));
                all = await DbSet.FindAsync(filter);

                //all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);

            }
            catch (Exception ex)
            {

                //throw;
            }
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual async Task<TEntity> GetByName(string name,string password, string finTheUser)
        {
            ConfigDbSet();
            //var xyz = Context.GetCollection<Product>("Product");
            //var filter = Builders<Product>.Filter.Eq("UnqueID", UnqueID);
            //var result = await xyz.FindAsync(filter);

            //IEnumerable<Product> listOfUsers;
            //var cursor = await xyz.FindAsync(x => x.UnqueID == UnqueID);
            //while (await cursor.MoveNextAsync())
            //{
            //    listOfUsers = cursor.Current;
            //}
            ////return listOfUsers.Current.Single();
            //var user = await xyz.Find(x => x.UnqueID == UnqueID).FirstAsync();
            var builder = Builders<TEntity>.Filter;
            var filter = builder.Eq("Username", name) & builder.Eq("Password", password);
            //var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("UnqueID", UnqueID));
            var data = await DbSet.FindAsync(filter);

           // var list = await DbSet.FindAsync((typeof(TEntity).Name)Builders<TEntity>.Filter.Eq(i => i.GetType().GetProperty("Username").GetValue(src, null)));

            return data.SingleOrDefault();
            //return user.
        }

        public virtual async Task<TEntity> GetByIdOnly(string UnqueID)
        {
            ConfigDbSet();
            //var xyz = Context.GetCollection<Product>("Product");
            //var filter = Builders<Product>.Filter.Eq("UnqueID", UnqueID);
            //var result = await xyz.FindAsync(filter);

            //IEnumerable<Product> listOfUsers;
            //var cursor = await xyz.FindAsync(x => x.UnqueID == UnqueID);
            //while (await cursor.MoveNextAsync())
            //{
            //    listOfUsers = cursor.Current;
            //}
            ////return listOfUsers.Current.Single();
            //var user = await xyz.Find(x => x.UnqueID == UnqueID).FirstAsync();
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("UnqueID", UnqueID));
            return data.SingleOrDefault();
            //return user.
        }

        public virtual async Task<TEntity> GetByIdDateStartOnly(string DateStart)
        {
            ConfigDbSet();

                       //var xyz = Context.GetCollection<Product>("Product");
            //var filter = Builders<Product>.Filter.Eq("UnqueID", UnqueID);
            //var result = await xyz.FindAsync(filter);

            //IEnumerable<Product> listOfUsers;
            //var cursor = await xyz.FindAsync(x => x.UnqueID == UnqueID);
            //while (await cursor.MoveNextAsync())
            //{
            //    listOfUsers = cursor.Current;
            //}
            ////return listOfUsers.Current.Single();
            //var user = await xyz.Find(x => x.UnqueID == UnqueID).FirstAsync();
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("DateStart", DateStart));
            return data.SingleOrDefault();
            //return user.
        }
         
        public virtual async Task<TEntity> Getanything(string paramVal)
        {
            ConfigDbSet();


            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("Name", paramVal));
            return data.SingleOrDefault();
            //return user.
        }

        public virtual async Task<IEnumerable<TEntity>> GetLoginDetails(string UName, string pwd)
        {
            ConfigDbSet();
            var builder = Builders<TEntity>.Filter;
            var filter = builder.Eq("UserName", UName) & builder.Eq("Password", pwd);

            var data = await DbSet.FindAsync(filter);
            //return data.SingleOrDefault();
            return data.ToList(); 
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            ConfigDbSet();
            IAsyncCursor<TEntity> all = null;
            try
            {
                all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);

            }
            catch (Exception ex)
            {

                //throw;
            }
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public async Task<List<NewModifyCase>> FindAll(Expression<Func<NewModifyCase, bool>> filter)
        {


            try
            {
                //Fetch the filtered list from the Collection
                List<NewModifyCase> documents = await DbSet1.Find(filter).ToListAsync();


                //return the list
                return documents;

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new List<NewModifyCase>() { });
            }

        }
        public virtual async Task<IEnumerable<TEntity>> GetAllCase()
        {
            ConfigDbSet();
            IAsyncCursor<TEntity> all = null;
            try
            {
                //CultureInfo invariantCulture1 = CultureInfo.InvariantCulture;
                
                //data = await DbSet1.FindAsync(filter);
                //all = await FindAll(builder.Gte(x => DateTime.ParseExact(x.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture), r6);
                all = await DbSet.FindAsync(Builders<TEntity>.Filter.Ne("CaseStatus" , "SoftDelete"));

            }
            catch (Exception ex)
            {

                //throw;
            }
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }
        public virtual async Task<IEnumerable<Expenses>> GetAllExpenses1(string noofdaysDate, string OneorMany, string[] dtFilterRange)
        {
            ConfigDbSet();
            IAsyncCursor<Expenses> data = null;
            try
            {
                string[] result = null; // new string[Convert.ToInt32(noofdaysDate)];
                if (OneorMany == "One")
                {
                    result = new string[1];
                    result[0] = noofdaysDate;
                }
                else if (OneorMany == "Many")
                {
                    result = new string[Convert.ToInt32(noofdaysDate)];
                    for (int i = 0; i < Convert.ToInt32(noofdaysDate); i++)
                    {
                        //var r7 = DateTime.ParseExact(DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //string pp = DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy").ToString();
                        //string pp1 = DateTime.Now.AddDays(-i).ToString("dd-MM-yyyy").ToString();
                        result[i] = DateTime.Now.AddDays(-i).ToString("dd-MM-yyyy").ToString();

                    }
                }
                else if (OneorMany == "Range")
                {
                    result = dtFilterRange;
                }
//                var p = DbSet2.AsQueryable().FirstAsync({ Date: { $elemMatch: { country: "01", "warehouse.code" : "02" } } }
//).pretty();
//                var filterDefinition = Builders<Expenses>.Filter.Where(t => result.Contains(t.Date.Substring(0, t.Date.IndexOf("T"))));
//                var filter = Builders<Expenses>.Filter.ElemMatch(x => x.Date, x=> result.Contains(x.ToString()));
                //DateTime dt = System.DateTime.Now;
                //System.debug('DATETIME:' + dt.TryFormat('yyyy-MM-dd\'T\'HH:mm:ss.SSS\'Z\''));
                //var filters = new List<FilterDefinition<Expenses>>();
                //filters.Add(Builders<Expenses>.Filter.In("Date", List<string>(){ a1,a2}));
                //filters.Add(Builders<Expenses>.Filter.Eq("Date", x => result.Contains(x.ToString())));
                ////var res = await collection.Find(filter).ToListAsync()
                //var filterDefinition = Builders<Expenses>.Filter.Regex("Date", "^" + result.Contains("Date") + ".*");
                var filterDefinition = Builders<Expenses>.Filter.Where(t => result.Contains(t.Date) );

                data = await DbSet2.FindAsync(filterDefinition);
                //all = await FindAll(builder.Gte(x => DateTime.ParseExact(x.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture), r6);
                //all = await DbSet.FindAsync(Builders<TEntity>.Filter.Ne("CaseStatus" , "SoftDelete"));

            }
            catch (Exception ex)
            {

                //throw;
            }
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return data.ToList();
        }
        public virtual async Task<IEnumerable<NewModifyCase>> GetAllCase1(string noofdaysDate, string OneorMany,string[] dtFilterRange)
        {
            ConfigDbSet();
            IAsyncCursor<TEntity> all = null;
            IAsyncCursor<NewModifyCase> data = null;
            try
            {
                string[] result = null; // new string[Convert.ToInt32(noofdaysDate)];
                if (OneorMany == "One")
                {
                    result = new string[1];
                    result[0] = noofdaysDate;
                }
                else if (OneorMany == "Many")
                {
                    result = new string[Convert.ToInt32(noofdaysDate)];
                    for (int i = 0; i < Convert.ToInt32(noofdaysDate); i++)
                    {
                        //var r7 = DateTime.ParseExact(DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //string pp = DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy").ToString();
                        //string pp1 = DateTime.Now.AddDays(-i).ToString("dd-MM-yyyy").ToString();
                        result[i] = DateTime.Now.AddDays(-i).ToString("dd-MM-yyyy").ToString();

                    }
                }
                else if (OneorMany == "Range")
                {
                    result = dtFilterRange;
                }
                    //      int noofdays = 15;
                    //      var start = new DateTime(2017, 03, 31);
                    //      var end = new DateTime(2017, 03, 31);
                    //      var r5 = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //      DateTime r6 = DateTime.ParseExact(DateTime.Now.AddDays(-noofdays).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture); //range setting
                    //      var builder = Builders<NewModifyCase>.Filter;
                    //      var filter = builder.Gte(x => DateTime.ParseExact(x.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture), r6) &
                    //builder.Lte(x => DateTime.ParseExact(x.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture), r5) &
                    //builder.Eq(x => x.CaseStatus, "SoftDelete");

                    //      var filter1 = builder.Gte(x => DateTime.ParseExact(x.DateStart, "dd/MM/yyyy", CultureInfo.InvariantCulture), r6) &
                    //builder.Lte(x => DateTime.ParseExact(x.DateStart, "dd/MM/yyyy", CultureInfo.InvariantCulture), r5) &
                    //builder.Eq(x => x.CaseStatus, "SoftDelete");
                    //      ArrayList authorsArray = new ArrayList();

                    //      for (int i = 0; i < Convert.ToInt32(noofdaysDate); i++)
                    //      {
                    //          //var r7 = DateTime.ParseExact(DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //          string pp = DateTime.Now.AddDays(-i).ToString("dd/MM/yyyy").ToString();
                    //          string pp1 = DateTime.Now.AddDays(-i).ToString("dd-MM-yyyy").ToString();
                    //          authorsArray.Add(pp.ToString().Replace("/", "-"));
                    //          result[i] = pp.ToString().Replace("/", "-");

                    //      }


                    //      var stringDate = "08-04-2021";
                    //      var myInClause = new string[] { "09-04-2021", "08-04-2021" ,"07-04-2021", "06-04-2021" };


                    //ArrayList authorsArray = new ArrayList();
                    //authorsArray.Add("09-04-2021");
                    //authorsArray.Add("08-04-2021");
                    //authorsArray.Add("07-04-2021");
                    //authorsArray.Add("06-04-2021");
                    var filterDefinition = Builders<NewModifyCase>.Filter.Where(t => result.Contains(t.DateStart) & t.CaseStatus != "SoftDelete");
                //var results = MyTable.Where(x => myInClause.Contains(x.SomeColumn));
                //var filterDefinition = Builders<NewModifyCase>.Filter.Where(t => t.DateStart == stringDate);


                // Filter Definition
                //List<NewModifyCase> documents = await Collection.Find(filter).ToListAsync();

                //var filter1 = Builders<TEntity>.Filter.Gt(x => x["DateStart"], oid);

                //var objModifyCase = p.Where(a => DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) <= r5
                //&& DateTime.ParseExact(a.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture) > r6).ToList();
                //var builder = Builders<NewModifyCase>.Filter;
                //var builder = Builders<TEntity>.Filter;
                //var filter = Builders<TEntity>.Filter.Gt(DateTime.ParseExact("11-11-2021".Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture), r6) & Builders<NewModifyCase>.Filter.Lt(DateTime.ParseExact("DateStart".Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture), r5) & builder.Eq("NewModifyCase", "SoftDelete");
                //var filter = builder.Gt("DateStart", name) & builder.Eq("Password", password);
                //var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("UnqueID", UnqueID));
                data = await DbSet1.FindAsync(filterDefinition);
                //all = await FindAll(builder.Gte(x => DateTime.ParseExact(x.DateStart.Replace("-", "/"), "dd/MM/yyyy", CultureInfo.InvariantCulture), r6);
                //all = await DbSet.FindAsync(Builders<TEntity>.Filter.Ne("CaseStatus" , "SoftDelete"));

            }
            catch (Exception ex)
            {

                //throw;
            }
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return data.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetStaffType(string StaffType)
        {
            ConfigDbSet();
            IAsyncCursor<TEntity> all = null;
            try
            {
                all = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("StaffType", StaffType));

            }
            catch (Exception ex)
            {

                //throw;
            }
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllCategoryWithType(string categoryType)
        {
            ConfigDbSet();
            IAsyncCursor<TEntity> all = null;
            try
            {
                all = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("CategoryType", categoryType));

            }
            catch (Exception ex)
            {

                //throw;
            }
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllPaymentHistory(string caseID)
        {
            ConfigDbSet();
            IAsyncCursor<TEntity> all = null;
            try
            {
                all = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("CaseID", caseID));

            }
            catch (Exception ex)
            {

                //throw;
            }
            //var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }



        public virtual void Update(TEntity obj, string uid)
        {
            ConfigDbSet();
            try
            {

                // var xyz = Context.GetCollection<Product>("Product");
                var filter = Builders<TEntity>.Filter.Eq("UnqueID", uid);

                //var result = await xyz.FindAsync(filter);
                //var replaceOneResult = xyz.ReplaceOneAsync( doc => doc.UnqueID == uid, obj, new UpdateOptions { IsUpsert = true });
                //return replaceOneResult;

                //Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("UnqueID", uid), obj, new UpdateOptions { IsUpsert = false }));
                Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("UnqueID", uid), obj));
            }
            catch (Exception ex)
            {

                //throw;
            }
            
        }

        public virtual void Remove(string uid)
        {
            ConfigDbSet();
            try
            {
                Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("UnqueID", uid)));
            }
            catch (Exception ex)
            {

                //throw;
            }
            
        }
        public virtual void RemoveBasedOnCASEID(string caseID)
        {
            ConfigDbSet();
            try
            {
                Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("CaseID", caseID)));
            }
            catch (Exception ex)
            {

                //throw;
            }

        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        
    }
}
