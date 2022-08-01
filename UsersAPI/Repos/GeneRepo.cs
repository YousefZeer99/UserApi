using Microsoft.EntityFrameworkCore;
using UsersAPI.Model;

namespace UsersAPI.Repos
{

    public interface IGeneRepo<T> where T : class
    {
        Task<List<T>> Get();
        public ValueTask<T?> GetId(int Id);

        public Task<T> Add(T ex);

        public T Update(T ex);

        public Task<T> Delete(int id);







    }
    public class GeneRepo<T> : IGeneRepo<T> where T : class , IBaseModel
    {
        private UserContext _context;

        public GeneRepo(UserContext context)
        {
            _context = context;
        }

        public async Task<List<T>> Get()
        {
            var ex = await _context.Set<T>().ToListAsync();
            return ex; 
        }

        public ValueTask<T?> GetId(int Id)
        {
            ValueTask<T?> ex = _context.Set<T>().FindAsync(Id);
            return ex;
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


        public async Task<T> Delete(int id)
        {
            var ex = await GetId(id);

            _context.Set<T>().Remove(ex);
            await _context.SaveChangesAsync();
            return ex; 








        }
    }
}