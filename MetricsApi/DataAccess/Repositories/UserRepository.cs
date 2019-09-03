using System;
using System.Collections.Generic;
using System.Linq;

//using Dapper.Contrib.Extensions;
using Dapper;
using Dapper.FastCrud;
using MetricsApi.DataAccess.EntityModels;

namespace MetricsApi.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDbConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public User GetUser(int id)
        {
            //var user = DbConnection.Get<User>(id);
            var users = DbConnection.Find<User>(statement => statement
                .Where($"{nameof(User.Id):C}=@Id")
                .WithParameters(new { Id = id }));

            return users.FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            //var users = DbConnection.GetAll<User>();
            var users = DbConnection.Find<User>();
            return users.ToList();
        }
    }
}
