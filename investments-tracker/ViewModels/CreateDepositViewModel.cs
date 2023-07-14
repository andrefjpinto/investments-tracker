namespace investments_tracker.ViewModels;

public class CreateDepositViewModel
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Guid BrokerId { get; set; }
}