using System;
using System.Collections.Generic;
using System.Linq;

using Dapper.Contrib.Extensions;
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
            var user = DbConnection.Get<User>(id);
            return user;
        }

        public List<User> GetUsers()
        {
            var users = DbConnection.GetAll<User>();
            return users.ToList();
        }
    }
}
