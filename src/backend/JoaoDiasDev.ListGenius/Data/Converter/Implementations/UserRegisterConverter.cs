using JoaoDiasDev.ListGenius.Data.Converter.Contract;
using JoaoDiasDev.ListGenius.Data.VO;
using JoaoDiasDev.ListGenius.Model;

namespace JoaoDiasDev.ListGenius.Data.Converter.Implementations
{
    public class UserRegisterConverter : IParser<UserRegisterVO, User>, IParser<User, UserRegisterVO>
    {
        public User Parse(UserRegisterVO origin)
        {
            if (origin == null)
            {
                return new User();
            }
            return new User
            {
                Email = origin.Email,
                Image = origin.Image,
                Password = origin.Password,
                Role = origin.Role,
                UserName = origin.UserName,
            };
        }

        public List<User> Parse(List<UserRegisterVO> origin)
        {
            if (origin == null)
            {
                return new List<User>();
            }
            return origin.Select(Parse).ToList();
        }

        public UserRegisterVO Parse(User origin)
        {
            if (origin == null)
            {
                return new UserRegisterVO();
            }
            return new UserRegisterVO
            {
                Email = origin.Email,
                Image = origin.Image,
                Password = origin.Password,
                Role = origin.Role,
                UserName = origin.UserName,
            };
        }

        public List<UserRegisterVO> Parse(List<User> origin)
        {
            if (origin == null)
            {
                return new List<UserRegisterVO>();
            }
            return origin.Select(Parse).ToList();
        }
    }
}
