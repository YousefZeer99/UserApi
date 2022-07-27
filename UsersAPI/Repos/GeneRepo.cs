using UsersAPI.Model;

namespace UsersAPI.Repos
{

    public interface IGeneRepo<T> where T : class
    {
        List<T> Get();
        public T GetId(int Id);

        public T Add(T ex);

        public T Update(T ex);

        public void Delete(int id);







    }
    public class GeneRepo<T> : IGeneRepo<T> where T : class , IBaseModel
    {
        private UserContext _context;

        public GeneRepo(UserContext context)
        {
            _context = context;
        }

        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public T GetId(int Id)
        {
            return _context.Find<T>(Id);
        }

        public T Add(T ex)
        {
            _context.Add<T>(ex);
            _context.SaveChanges();
            return ex;

        }

        public T Update(T ex)
        {
            _context.Update<T>(ex);
            _context.SaveChanges();
            return ex;




        }


        public void Delete(int id)
        {
            var ex = GetId(id);

            _context.Remove<T>(ex);
            _context.SaveChanges();









        }
    }
}