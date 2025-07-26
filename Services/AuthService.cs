using SmartFridgeApi.Models;
using SmartFridgeApi.Data;
using System.Security.Cryptography;
using System.Text;

namespace SmartFridgeApi.Services
{
    public class AuthService
    {
        private readonly SmartFridgeDbContext _context;

        public AuthService(SmartFridgeDbContext context)
        {
            _context = context;
        }

        public User? GetUserByEmail(string email)
            => _context.Users.FirstOrDefault(u => u.Email == email);

        public User Register(string email, string password)
        {
            var passwordHash = HashPassword(password);
            var user = new User { Email = email, PasswordHash = passwordHash };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool ValidatePassword(string email, string password)
        {
            var user = GetUserByEmail(email);
            return user != null && user.PasswordHash == HashPassword(password);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        // CORRECTION ICI : Utilise la base !
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
