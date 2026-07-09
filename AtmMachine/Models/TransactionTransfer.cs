namespace AtmMachine.Models
{
    public class TransactionTransfer : Transactions
    {
        public int SenderAccountId { get; set; }
        public Account SenderAccount { get; set; }

        public int ReceiverAccountId { get; set; }
        public Account ReceiverAccount { get; set; }
    }

}
