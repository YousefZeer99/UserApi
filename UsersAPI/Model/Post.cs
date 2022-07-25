using System.ComponentModel.DataAnnotations.Schema; 


namespace UsersAPI.Model
{
    public class Post
    {
        public int Id { get; set; }

        public string Ptitle { get; set; }

        [ForeignKey("UId")]

        public int UId { get; set; }

        public User? u { get; set; }

    }
}
