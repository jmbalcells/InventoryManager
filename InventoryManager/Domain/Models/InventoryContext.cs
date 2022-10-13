using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace InventoryManager.Domain.Models
{
    public class InventoryContext: DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; } = null!;
    }
}
