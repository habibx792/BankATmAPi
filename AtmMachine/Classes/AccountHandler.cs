using System.CodeDom;
using System.Security.Cryptography;
using System.Text;

namespace AtmMachine.Classes
{
    public static class AccountHandler
    {
        public static bool isValidAtmPin(string pin)
        {
            if(pin==""||pin.Length!=4)
            {
                return false;
            }
            return true;
        }
        public static bool  isValidChic(string cnic)
        {
            if(cnic.Length>13||cnic=="")
            {
                return false;
            }
            return true;
        }
        public static bool isValidPassword(string passWord)
        {
            if(passWord==""||passWord.Length<8||passWord.Length>12)
            {
                return false;
            }
            return true;
        }
        public static string GenerateRandomTransactionNumber(int len = 10)
        {
            StringBuilder sb = new StringBuilder(len);

            for (int i = 0; i < len; i++)
            {
                sb.Append(RandomNumberGenerator.GetInt32(10));
            }

            return sb.ToString();
        }

    }
}
