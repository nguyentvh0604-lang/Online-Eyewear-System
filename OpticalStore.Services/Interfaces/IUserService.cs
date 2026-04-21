using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpticalStore.Repositories.Models;

namespace OpticalStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> LoginAsync(string email, string password);
        Task<User> RegisterAsync(string fullName, string email, string phone, string password);
    }
}