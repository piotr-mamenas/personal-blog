using System;
using PersonalBlog.Web.Core;
using PersonalBlog.Web.Core.Domain;
using PersonalBlog.Web.Core.Repositories;
using PersonalBlog.Web.Persistence;
using PersonalBlog.Web.Persistence.Repositories;

namespace PersonalBlog.Web.Helpers
{
    public static class TemporaryDatabaseSeeder
    {
        public static void Seed()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            IRepository<Post> postRepository = new Repository<Post>(unitOfWork);
            IRepository<User> userRepository = new Repository<User>(unitOfWork);

            var post = new Post
            {
                Timestamp = DateTime.Today,
                Body = "Post Body",
                Title = "A titled post",
                Id = 1
            };
            postRepository.Create(post);

            var user = new User
            {
                Email = "piotr.mamenas@gmail.com",
                Id = 1,
                PasswordHash = SecurePasswordHasher.Hash("admin"),
                Username = "admin"
            };
            userRepository.Create(user);
        }

    }
}