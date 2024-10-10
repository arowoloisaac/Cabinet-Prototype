using System.Text;

namespace Cabinet_Prototype.Services.Initialization.PasswordGenerator
{
    public class PasswordGen: IPasswordGen
    {
        private readonly string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly string LowerCase = "abcdefghijklmnopqrstuvwxyz";

        private readonly string Digits = "0123456789";


        public string GeneratePassword(int length)
        {
            if (length < 8)
            {
                throw new ArgumentException("Password length must be at least 8 characters.");
            }

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            password.Append(UpperCase[random.Next(UpperCase.Length)]);
            password.Append(LowerCase[random.Next(LowerCase.Length)]);
            password.Append(Digits[random.Next(Digits.Length)]);

            string allCharacters = UpperCase + LowerCase + Digits;

            for (int i = 4; i < length; i++)
            {
                password.Append(allCharacters[random.Next(allCharacters.Length)]);
            }

            return ShufflePassword(password.ToString());
        }

        private static string ShufflePassword(string password)
        {
            char[] array = password.ToCharArray();
            Random random = new Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                char temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            return new string(array);
        }
    }
}
