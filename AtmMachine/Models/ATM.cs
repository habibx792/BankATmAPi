

using AtmMachine.Classes;

namespace AtmMachine.Models
{
    public class ATM :BaseModel, IATM
    {
        public string AtmNumber { get; private set; }
        public Account Account { get; set; }
        
        public string AtmPin { get; private set; } = "1234";

        public  double CheckBalance()
        {
            return Account.Balance;
        }
        public bool SetAtmPassword(string atmPin)
        {
            if (AccountHandler.isValidAtmPin(atmPin))
            {
                this.AtmPin = atmPin;
                return true;
            }
            return false;
        }
        public bool DepositeIsPossible(double amount)
        {
            if(amount<=0)
            {
                return false;
            }
            return true;
        }
        public bool Deposite(double amount)
        {
            if(DepositeIsPossible(amount))
            {
                this.Account.UpdateBalance(this.Account.Balance + amount)
                    ;
                return true
                    ;
            }
            return false;
        }
       public bool WithIsPossible(double amount)
        {
            if(this.Account.Balance<amount)
            {
                return false;
            }
            return true;
        }
      public bool withDraw(double amount)
        {
            if(WithIsPossible(amount))

            {
                this.Account.UpdateBalance(this.Account.Balance - amount);
                return true;
            }
            return false;

        }


    }
}
