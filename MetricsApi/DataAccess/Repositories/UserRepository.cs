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

        public UserEntity GetUser(int id)
        {
            //var user = DbConnection.Get<UserEntity>(id);
            var users = DbConnection.Find<UserEntity>(statement => statement
                .Where($"{nameof(UserEntity.Id):C}=@Id")
                .WithParameters(new { Id = id }));

            return users.FirstOrDefault();
        }

        public List<UserEntity> GetUsers()
        {
            //var users = DbConnection.GetAll<User>();
            var users = DbConnection.Find<UserEntity>();
            return users.ToList();
        }
    }
}
