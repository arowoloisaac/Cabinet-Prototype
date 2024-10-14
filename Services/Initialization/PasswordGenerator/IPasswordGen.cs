namespace Cabinet_Prototype.Services.Initialization.PasswordGenerator
{
    public interface IPasswordGen
    {
        string GeneratePassword(int length);
    }
}
