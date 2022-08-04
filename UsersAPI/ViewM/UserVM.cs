using UsersAPI.Model;

namespace UsersAPI.ViewM
{
    public class UserVM : BaseModel
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
