using Microsoft.EntityFrameworkCore;
using AtmMachine.Classes;

namespace AtmMachine.Models
{
    public class Account : BaseModel
    {
        public int PersonId { get; set; }

        public Person Person { get; set; }

        public double Balance { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public string AccountPassword { get; set; }


        public void UpdateBalance(double amount)
        {
            this.Balance = amount;
        }
        public bool DepositAmount(double amount)
        {
            if(amount>0)
            {
                this.Balance += amount;
                return true;
            }
            return false;
          
        }
        public bool WithDraw(double amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                this.Balance -= amount;
                return true;
            }
            return false;
        }
       

        
        public bool SetPassword(string password)
        {
            if (AccountHandler.isValidPassword(password))
            {
                this.AccountPassword = password;
                return true;
            }
            return false;
        }

    }
}
