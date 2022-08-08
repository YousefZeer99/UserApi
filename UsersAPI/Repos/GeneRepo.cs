using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Model;

namespace UsersAPI.Repos
{

    public interface IGeneRepo<T> where T : class
    {
        Task<List<TVM>> Get<TVM>() where TVM : class, IBaseModel;
        public ValueTask<TVM?> GetId<TVM>(int Id) where TVM : class, IBaseModel; 

        public Task<T> Add(T ex , int userID);

        public T Update(T ex , int userID);

        public Task<TVM> Delete<TVM>(int id) where TVM : class, IBaseModel;







    }
    public class GeneRepo<T> : IGeneRepo<T> where T : class , IBaseModel
    {
        private readonly UserContext _context;
        private readonly IMapper _imapper; 

        public GeneRepo(UserContext context , IMapper immaper )
        {
            _context = context;
            _imapper = immaper; 
        }

        public async Task<List<TVM>> Get<TVM>() where TVM : class , IBaseModel
        {
            return await _context.Set<T>().ProjectTo<TVM>(_imapper.ConfigurationProvider).ToListAsync();
        }

        public  async ValueTask<TVM?> GetId<TVM>(int id) where TVM : class , IBaseModel
        {
            return await _context.Set<T>().ProjectTo<TVM>(_imapper.ConfigurationProvider).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<T> Add(T ex, int userID)
        {
           var type = ex.GetType();
           var CreateDate = type.GetProperties().FirstOrDefault(x => x.Name == "CreateDate");
           var CreateByDate = type.GetProperties().FirstOrDefault(x => x.Name == "CreateByDate"); 
           if (CreateDate != null)
            {
                var DateNow = DateTime.Now; 
                CreateDate.SetValue(ex, DateNow);
                CreateByDate.SetValue(ex, userID); 

            }




           await _context.Set<T>().AddAsync(ex);
           await _context.SaveChangesAsync();
            return ex;

        }

        public T Update(T ex, int userID)
        {

            var type = ex.GetType();
            var UpdateDate = type.GetProperties().FirstOrDefault(x => x.Name == "UpdateDate");
            var UpdateByDate = type.GetProperties().FirstOrDefault(x => x.Name == "UpdateByDate");

            if(UpdateDate != null)
            {
                UpdateDate.SetValue(ex, DateTime.Now);
                UpdateByDate.SetValue(ex, userID);

                var CreatedDate = type.GetProperties().FirstOrDefault(x => x.Name == "CreateDate");
                var CreatedByDate = type.GetProperties().FirstOrDefault(x => x.Name == "CreateByDate");


                // to get needed records 

                var record = _context.posts.Select(
                    c => new
                    {
                        id = c.Id,
                        CreatedDate = c.CreateDate,
                        CreatedBy = c.CreateByDate,

                    }
                    ).FirstOrDefault(x => x.id == ex.Id);


                CreatedDate.SetValue(ex, record.CreatedDate);
                CreatedByDate.SetValue(ex, record.CreatedBy); 
            }

            _context.Update<T>(ex);
            _context.SaveChanges();
            return ex;




        }


        public async Task<TVM> Delete<TVM>(int id) where TVM:class,IBaseModel
        {
            var ex = await GetId<TVM>(id);

            _context.Set<T>().Remove(_imapper.Map<T>(ex));
            await _context.SaveChangesAsync();
            return _imapper.Map<TVM>(ex); 








        }

      
    }
}