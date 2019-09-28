using System;
using System.Collections.Generic;
using System.Linq;

//using Dapper.Contrib.Extensions;
using Dapper;
using Dapper.FastCrud;
using MetricsApi.DataAccess.EntityModels;

namespace MetricsApi.DataAccess.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(IDbConnectionHelper connectionHelper) 
            : base(connectionHelper)
        {
        }

        public UserEntity GetById(int id)
        {
            //var user = DbConnection.Get<UserEntity>(id);
            var users = DbConnection.Find<UserEntity>(statement => statement
                .Where($"{nameof(UserEntity.Id):C}=@Id")
                .WithParameters(new { Id = id }));

            return users.FirstOrDefault();
        }

        public IEnumerable<UserEntity> GetAll()
        {
            var users = DbConnection.Find<UserEntity>();
            return users;
        }

        public UserEntity Create(UserEntity userEntity)
        {
            DbConnection.Insert<UserEntity>(userEntity);
            return userEntity;
        }

        public void Update(UserEntity userEntity)
        {
            DbConnection.Update<UserEntity>(userEntity);
        }

        public void Delete(int id)
        {
            var isDeleted = DbConnection.Delete<UserEntity>(new UserEntity { Id = id });
        }
    }
}
