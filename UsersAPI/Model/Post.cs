using System.ComponentModel.DataAnnotations.Schema; 


namespace UsersAPI.Model
{
    public class Post : BaseModel
    {
        

        public string Ptitle { get; set; }

        [ForeignKey("UId")]

        public int UId { get; set; }

        public User? u { get; set; }

        public DateTime CreateDate { get; set; }    

        public int CreateByDate { get; set;  }

        public DateTime UpdateDate { get; set; }

        public int UpdateByDate { get; set; }   





    }
}
