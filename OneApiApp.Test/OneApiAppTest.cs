using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OneApiApp.Test
{
    [TestClass]
    public class OneApiAppTest
    {
        [TestMethod]
        public async Task GoodInputTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("5");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("0, 1, 1, 2, 3, 5"));
        }

        [TestMethod]
        public async Task WrongInputTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("ee3");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains(" Wrong Input Provided."));
        }

        [TestMethod]
        public async Task TooBigInputTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("55");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("The number is too big."));
        }

    }
}
