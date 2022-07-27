﻿using UsersAPI.Model;

namespace UsersAPI.Repos
{
    public interface INewPostRepo : IGeneRepo<Post>
    {

    }

    public class NewPostRepo : GeneRepo<Post>, INewPostRepo
    {
        public NewPostRepo(UserContext context) : base(context)
        {
        }
    }
}