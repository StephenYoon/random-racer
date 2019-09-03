using System;
using System.Collections.Generic;
using System.Linq;
using MetricsApi.DataAccess.EntityModels;

namespace MetricsApi.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(int id);

        List<User> GetUsers();
    }
}
