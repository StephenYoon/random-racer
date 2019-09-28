using System;
using System.Collections.Generic;
using System.Linq;
using MetricsApi.DataAccess.EntityModels;

namespace MetricsApi.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity GetById(int id);

        IEnumerable<UserEntity> GetAll();

        UserEntity Create(UserEntity userEntity);

        void Update(UserEntity userEntity);

        void Delete(int id);
    }
}
