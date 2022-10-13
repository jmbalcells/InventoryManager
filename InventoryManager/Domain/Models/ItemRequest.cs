namespace InventoryManager.Domain.Models
{
    public record ItemRequest
    {
        public string Name { get; init; }
        public string Type { get; init; }
    }
}
