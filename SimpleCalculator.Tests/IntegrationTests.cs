using System.Net.Http.Json;

public class IntegrationTests{
    [Test]
    public async Task When_PostCalculate_SimpleAdd_Should_Be_Expected_Result()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => {});
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/calculate", new Equation("1+2"));
        var actual = await response.Content.ReadAsStringAsync();

        Assert.That(actual, Is.EqualTo("1+2 OK"));
    }
}