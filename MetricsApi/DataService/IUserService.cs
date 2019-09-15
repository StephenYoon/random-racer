using System;
using System.Collections.Generic;
using System.Linq;
using MetricsApi.Models;

namespace MetricsApi.DataService
{
    public interface IUserService
    {
        User GetUser(int id);

        List<User> GetUsers();
    }
}
