using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Model;
using JoaoDiasDev.ListGenius.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace JoaoDiasDev.ListGenius.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;
        private DbSet<User> _dataset;

        public UserRepository(MySQLContext context)
        {
            _context = context;
            _dataset = _context.Set<User>();
        }
        public User ValidateCredentials(UserVO user)
        {
            try
            {
                var password = ComputeHash(user.Password);
                return _context.Users?.FirstOrDefault(u => (u.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)) && (u.Password.Equals(password))) ?? throw new InvalidOperationException("Incorret credentials entered.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id)))
            {
                return new User();
            }
            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result ?? new User();
        }

        private object ComputeHash(string input)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = SHA256.HashData(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

        public User ValidateCredentials(string userName, string email)
        {
            return _context.Users?
                .SingleOrDefault(u => u.UserName == userName || u.Email == email) ?? new User();
        }

        public bool RevokeToken(string userName)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
                if (user is null)
                {
                    return false;
                }

                user.RefreshToken = string.Empty;

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateUser(User user)
        {
            try
            {
                if (user is null) return false;

                user.Password = ComputeHash(user.Password).ToString() ?? string.Empty;
                user.FullName = user.UserName.ToUpper();

                if (string.IsNullOrEmpty(user.Password)) return false;

                _dataset.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
