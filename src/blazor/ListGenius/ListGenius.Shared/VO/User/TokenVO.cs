﻿namespace ListGenius.Shared.VO.User
{
    public class TokenVO
    {
        public TokenVO(string accessToken, string refreshToken, bool authenticated = true, string created = "", string expiration = "")
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}