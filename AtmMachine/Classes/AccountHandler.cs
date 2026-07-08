using System.CodeDom;

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

    }
}
