namespace AtmMachine.Models
{
    public class AccountDetailsDto
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public string AccountPassword { get; set; }
        public string ModifiedBy { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNIC { get; set; }
    }
}
