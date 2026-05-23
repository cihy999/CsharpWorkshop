namespace P02_Delegate.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public ICollection<Card> Cards { get; set; } = [];

    public override string ToString() =>
        $"Id = {Id}, Name = {Name}, Address = {Address}";
}
