namespace AtmMachine.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt
            { get; set; }
        public string? ModifiedBy{ get; set; }
    }
}
