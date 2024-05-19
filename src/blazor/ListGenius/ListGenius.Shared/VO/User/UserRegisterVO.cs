namespace ListGenius.Shared.VO.User
{
    public class UserRegisterVO
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public byte[] Image { get; set; } = [];

    }
}
