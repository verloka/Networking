namespace SignalRChatShare.Models
{
    public class AuthenticateResponse
    {
        public User User { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User User, string Token)
        {
            this.User = User;
            this.Token = Token;
        }
    }
}
