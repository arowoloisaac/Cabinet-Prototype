namespace Cabinet_Prototype.Models
{
    public class Message
    {
        public string _message { get; set; } = string.Empty;

        public Message(string message)
        {
            _message = message;
        }
    }
}
