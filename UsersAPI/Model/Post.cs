using System.ComponentModel.DataAnnotations.Schema; 


namespace UsersAPI.Model
{
    public class Post : BaseModel
    {
        

        public string Ptitle { get; set; }

        [ForeignKey("UId")]

        public int UId { get; set; }

        public User? u { get; set; }

    }
}
