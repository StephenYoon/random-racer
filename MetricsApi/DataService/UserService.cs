using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MetricsApi.DataAccess.Repositories;
using MetricsApi.DataAccess.EntityModels;
using MetricsApi.Models;
using MetricsApi.Utilities;
using System.Text;

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

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var userEntity = _userRepository.GetAll().SingleOrDefault(x => x.EmailAddress == email);

            // check if username exists
            if (userEntity == null)
            {
                return null;
            }

            // check if password is correct
            if (!VerifyPasswordHash(password, userEntity.PasswordHash, userEntity.PasswordSalt))
            {
                return null;
            }

            // authentication successful
            var user = _mapper.Map<User>(userEntity);
            return user;
        }

        public User GetById(int id)
        {
            var userEntity = _userRepository.GetById(id);
            var user = _mapper.Map<User>(userEntity);

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            var userEntities = _userRepository.GetAll();
            var users = _mapper.Map<List<User>>(userEntities);

            return users;
        }
        
        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }

            if (_userRepository.GetAll().Any(x => x.EmailAddress == user.EmailAddress))
            {
                throw new AppException("Email \"" + user.EmailAddress + "\" is already taken");
            }

            // generate password hash
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // add new user
            var userEntity = _mapper.Map<UserEntity>(user);
            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;
            userEntity.Created = DateTime.Now;
            _userRepository.Create(userEntity);

            // return newly added user with Id
            var insertedUser = _mapper.Map<User>(userEntity);
            return insertedUser;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == userParam.Id);

            if (user == null)
            {
                throw new AppException("User not found");
            }

            if (userParam.EmailAddress != user.EmailAddress)
            {
                // EmailAddress has changed so check if the new EmailAddress is already taken
                if (_userRepository.GetAll().Any(x => x.EmailAddress == userParam.EmailAddress))
                {
                    throw new AppException("EmailAddress " + userParam.EmailAddress + " is already taken");
                }
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.EmailAddress = userParam.EmailAddress;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _userRepository.Update(user);
        }

        public void Delete(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                _userRepository.Delete(id);
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSaltBytes = hmac.Key;
                var passwordHashBytes = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                passwordSalt = passwordSaltBytes;
                passwordHash = passwordHashBytes;
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private static string ConvertBytesToString(byte[] bytes, StringEncodingEnum encoding = StringEncodingEnum.EncodingUTF8)
        {
            //// Merge all bytes into a string of bytes  
            //var builder = new StringBuilder();
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    builder.Append(bytes[i].ToString("x2"));
            //}
            //var conversion = builder.ToString();

            //var conversion = BitConverter.ToString(bytes);

            var conversion = encoding == StringEncodingEnum.EncodingUTF8
                ? Encoding.UTF8.GetString(bytes, 0, bytes.Length)
                : Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            return conversion;
        }

        private static byte[] ConvertStringToBytes(string input, StringEncodingEnum encoding = StringEncodingEnum.EncodingUTF8)
        {
            var conversion = encoding == StringEncodingEnum.EncodingUTF8
                ? Encoding.UTF8.GetBytes(input)
                : Encoding.ASCII.GetBytes(input);
            return conversion;
        }
    }
}
