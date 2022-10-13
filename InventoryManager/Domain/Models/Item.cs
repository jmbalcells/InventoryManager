using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Domain.Models
{
    public record Item
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Type { get; init; }
        public DateTime Expiration { get; init; }
        public DateTime Created { get; init; }
        public DateTime Updated { get; init; }
    }
}
