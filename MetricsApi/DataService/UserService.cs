using System;
using System.Collections.Generic;
using System.Linq;
using MetricsApi.DataAccess.Repositories;
using MetricsApi.DataService.Models;

namespace MetricsApi.DataService
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUser(int id)
        {
            var user = _userRepository.GetUser(id);

            // TODO: use AutoMapper to map EntityModels to Service.Models
            return new User { Id = id, FirstName = "Chevy", LastName = "Camaro" };
        }

        public List<User> GetUsers()
        {
            var users = _userRepository.GetUsers();

            // TODO: use AutoMapper to map EntityModels to Service.Models
            return new List<User> { new User { Id = 123, FirstName = "Chevy", LastName = "Camaro" } };
        }
    }
}
