using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Confluent.Kafka;
using Infraestructure.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using InventoryManager.Domain.Models;

namespace InventoryManager.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ItemsController : ControllerBase
    {
        private readonly InventoryContext _context;
        private readonly IConfiguration _configuration;

        public ItemsController(InventoryContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Get all items in memory
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Item>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        /// <summary>
        /// Get details from Item by name
        /// </summary>
        /// <param name="name"></param>
        [HttpGet("Detail")]
        public async Task<ActionResult<Item>> GetItem(string name)
        {
            if (name == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Name == name);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        /// <summary>
        /// Create new item
        /// </summary>
        /// <param name="newItem"></param>
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(ItemRequest newItem)
        {
            var item = new Item
            {
                Name = newItem.Name,
                Type = newItem.Type,
                Expiration = DateTime.Now.AddDays(5),
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        /// <summary>
        /// Delete an item by name
        /// </summary>
        /// <param name="name"></param>
        [HttpPost("Take")]
        public async Task<ActionResult> Take(string name)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'InventoryContext.Items'  is null.");
            }
            var item = _context.Items.Where(i => i.Name == name).FirstOrDefault();
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();

            using (var producer = new ProducerBuilder<string, string>(KafkaConfig.config).Build())
            {
                producer.ProduceAsync(_configuration["KafkaConfig:Topic"], new Message<string, string> { Key = "Delete", Value = "variable_item.id" });
            }

            return RedirectToAction(nameof(GetItems));
        }
    }
}
