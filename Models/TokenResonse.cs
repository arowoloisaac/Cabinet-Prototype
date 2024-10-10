namespace Cabinet_Prototype.Models
{
    public class TokenResonse
    {
        public string? Token { get; set; }

        public TokenResonse(string Token)
        {
            this.Token = Token;
        }
    }
}
