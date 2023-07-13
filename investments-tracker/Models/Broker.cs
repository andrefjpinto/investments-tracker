namespace investments_tracker.Models;

public class Broker : BaseEntity
{
    public string Name { get; set; }
    public Uri Website { get; set; }
}