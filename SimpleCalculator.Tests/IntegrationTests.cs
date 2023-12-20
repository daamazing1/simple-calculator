using System.Dynamic;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class IntegrationTests{
    [Test]
    public async Task When_PostCalculate_SimpleAdd_Should_Be_Expected_Result()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => {});
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/calculate", new Equation("1+2"));
        var actual = JsonConvert.DeserializeObject<ExpandoObject>(await response.Content.ReadAsStringAsync());

        IDictionary<string, object> actualDictionary = actual;
        Assert.That((double) actualDictionary["result"], Is.EqualTo(3.0));
    }
}