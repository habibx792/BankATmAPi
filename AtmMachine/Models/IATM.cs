namespace AtmMachine.Models
{
    public interface IATM
    {
        bool DepositeIsPossible(double amount);
        double CheckBalance();
        bool Deposite(double amount);
        bool WithIsPossible(double amount);
        bool withDraw(double amount);

    }
}
