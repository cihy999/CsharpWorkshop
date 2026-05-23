namespace P02_Delegate.Models;

public class Card
{
    public int Id { get; set; }
    public string HolderName { get; set; } = string.Empty;
    public string ExpiryDate { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public int CustomerId { get; set; }

    public override string ToString() =>
        $"Id = {Id}, HolderName = {HolderName}, Number = {Number}, ExpiryDate = {ExpiryDate}, CustomerId = {CustomerId}";
}
