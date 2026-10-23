using P02_Delegate.Models;

namespace P02_Delegate;

/// <summary>
/// 記憶體中的示範資料，取代教學影片裡的資料庫查詢。
/// </summary>
internal static class CardData
{
    public static IReadOnlyList<Card> All { get; } =
    [
        new() { Id = 1, HolderName = "Mr Thomas", ExpiryDate = "12/26", Number = "4111-1111-1111-1111", CustomerId = 1 },
        new() { Id = 2, HolderName = "Alice Chen", ExpiryDate = "01/27", Number = "4222-2222-2222-2222", CustomerId = 2 },
        new() { Id = 3, HolderName = "Hanma Baki", ExpiryDate = "05/28", Number = "4333-3333-3333-3333", CustomerId = 2 },
        new() { Id = 4, HolderName = "Bob Miller", ExpiryDate = "03/28", Number = "4444-4444-4444-4444", CustomerId = 3 },
        new() { Id = 5, HolderName = "Carol Lee", ExpiryDate = "06/29", Number = "4555-5555-5555-5555", CustomerId = 3 },
        new() { Id = 6, HolderName = "Dave Wilson", ExpiryDate = "09/30", Number = "4666-6666-6666-6666", CustomerId = 4 },
        new() { Id = 7, HolderName = "Eve Adams", ExpiryDate = "11/31", Number = "4777-7777-7777-7777", CustomerId = 4 },
    ];
}
