
using Microsoft.AspNetCore.Identity;

namespace UsersAPI.Model
{
    public class User :IdentityUser<int>, IBaseModel
    {
        
       
        public string FName { get; set; }
        public string LName { get; set; }

        public ICollection<Post> posts { get; set; }
      

    }
}
