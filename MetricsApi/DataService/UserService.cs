using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MetricsApi.DataAccess.Repositories;
using MetricsApi.DataAccess.EntityModels;
using MetricsApi.Models;

namespace MetricsApi.DataService
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User GetById(int id)
        {
            var userEntity = _userRepository.GetUser(id);
            var user = _mapper.Map<User>(userEntity);

            return user;
        }

        public List<User> GetAll()
        {
            var userEntities = _userRepository.GetUsers();
            var users = _mapper.Map<List<User>>(userEntities);

            return users;
        }
    }
}
