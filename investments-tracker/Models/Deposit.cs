namespace investments_tracker.Models;

public class Deposit : BaseEntity
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Guid BrokerId { get; set; }
    
    public virtual Broker Broker { get; set; }
}