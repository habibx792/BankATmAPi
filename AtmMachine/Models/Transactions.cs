using AtmMachine.Classes;
using Microsoft.Identity.Client;

namespace AtmMachine.Models
{
    public class Transactions:BaseModel
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string TransactionId { get; set; } = AccountHandler.GenerateRandomTransactionNumber();
        public double AccountBalance { get; set; }
        public bool IsSuccessfull = true;

    }

}
