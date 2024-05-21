using ListGenius.Domain.Data.Converter.Contract;
using ListGenius.Domain.Model;
using ListGenius.Shared.VO.User;

namespace ListGenius.Domain.Data.Converter.Implementations
{
    public class UserRegisterConverter : IParser<UserRegisterVO, UserIdentity>, IParser<UserIdentity, UserRegisterVO>
    {
        public UserIdentity Parse(UserRegisterVO origin)
        {
            if (origin == null)
            {
                return new UserIdentity();
            }
            return new UserIdentity
            {
                Email = origin.Email,
                Image = origin.Image,
                Password = origin.Password,
                Role = origin.Role,
                UserName = origin.UserName,
            };
        }

        public List<UserIdentity> Parse(List<UserRegisterVO> origin)
        {
            if (origin == null)
            {
                return new List<UserIdentity>();
            }
            return origin.Select(Parse).ToList();
        }

        public UserRegisterVO Parse(UserIdentity origin)
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

        public List<UserRegisterVO> Parse(List<UserIdentity> origin)
        {
            if (origin == null)
            {
                return new List<UserRegisterVO>();
            }
            return origin.Select(Parse).ToList();
        }
    }
}
