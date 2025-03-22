using Microsoft.EntityFrameworkCore;
using DevicesBackend.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DevicesBackend.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;

        private const string DEFAULT_USER_LOGIN = "admin";

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            CreateDefaultUserIfNecessary();
        }


        private void CreateDefaultUserIfNecessary()
        {
            if (_dbContext.Users.FirstOrDefault(u => u.Login == DEFAULT_USER_LOGIN) != null)
                return;

            byte[] salt = CreateSalt();
            string passwordHash = HashPassword(password: "admin", salt);

            User defaultUser = new User
            {
                Login = "admin",
                Salt = salt,
                PasswordHash = passwordHash
            };

            _dbContext.Users.Add(defaultUser);
            _dbContext.SaveChanges();
        }

        public async Task<bool> CheckLoginAndPassword(string login, string password)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user == null)
                return false;

            if (HashPassword(password, user.Salt) == user.PasswordHash)
                return true;
            else
                return false;
        }

        private byte[] CreateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            return salt;
        }

        private string HashPassword(string password, byte[] salt)
        {
            byte[] key = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 128 / 8);
            string hash = Convert.ToBase64String(key);
            return hash;
        }
    }
}
