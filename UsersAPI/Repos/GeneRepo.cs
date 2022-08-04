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

        public Task<T> Add(T ex);

        public T Update(T ex);

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

        public async Task<T> Add(T ex)
        {
           await _context.Set<T>().AddAsync(ex);
           await _context.SaveChangesAsync();
            return ex;

        }

        public T Update(T ex)
        {
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