using Bogus;
using InventoryManager.Controllers;
using InventoryManager.Domain.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test
{
    public class InventoryManagerTest
    {
        [Fact]
        public async Task PostItem_Not_Be_Null()
        {

            var mockContext = new Mock<InventoryContext>();
            var mockConfiguration = new Mock<IConfiguration>();

            var controller = new ItemsController(mockContext.Object, mockConfiguration.Object);

            var fakeItemRequest = new Faker<ItemRequest>()
                .RuleFor(m => m.Name, f => f.Random.String2(8))
                .RuleFor(m => m.Type, f => f.Random.String2(8));

            var result = await controller.PostItem(fakeItemRequest);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Take_Be_Null()
        {

            var mockContext = new Mock<InventoryContext>();
            var mockConfiguration = new Mock<IConfiguration>();

            var controller = new ItemsController(mockContext.Object, mockConfiguration.Object);

            var fakeItemRequest = new Faker<ItemRequest>()
                .RuleFor(m => m.Name, "item1")
                .RuleFor(m => m.Type, f => f.Random.String2(8));

            await controller.PostItem(fakeItemRequest);

            var result = await controller.Take("item1");

            Assert.Null(result);
        }
    }
}
