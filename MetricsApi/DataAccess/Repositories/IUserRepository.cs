using System;
using System.Collections.Generic;
using System.Linq;
using MetricsApi.DataAccess.EntityModels;

namespace MetricsApi.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity GetUser(int id);

        List<UserEntity> GetUsers();
    }
}
