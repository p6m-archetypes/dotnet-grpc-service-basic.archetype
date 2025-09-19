using Grpc.Core;
using {{ PrefixName }}{{ SuffixName }}.API;
using {{ PrefixName }}{{ SuffixName }}.Client;
using Xunit.Abstractions;
using Xunit;

namespace {{ PrefixName }}{{ SuffixName }}.IntegrationTests;

[Collection("ApplicationCollection")]
public class {{ PrefixName }}{{ SuffixName }}GrpcIt(ITestOutputHelper testOutputHelper, ApplicationFixture applicationFixture)
{
    private readonly ApplicationFixture _applicationFixture = applicationFixture;
    private readonly {{ PrefixName }}{{ SuffixName }}Client _client = applicationFixture.GetClient();
    [Fact]
    public async Task Test_Create{{ PrefixName }}()
    {
        //Arrange
    
        //Act
        var createRequest = new {{ PrefixName }}Dto { Name = Guid.NewGuid().ToString() };
        var response = await _client.Create{{ PrefixName }}(createRequest);
        
        //Assert
        var dto = response.{{ PrefixName }};
        Assert.NotNull(dto.Id);
        Assert.Equal(createRequest.Name, dto.Name);
    }
    
    [Fact]
    public async Task Test_Get{{ PrefixName }}s()
    {
        testOutputHelper.WriteLine("Test_Get{{ PrefixName }}s");
        
        //Arrange
        var beforeTotal = (await _client.Get{{ PrefixName }}s(new Get{{ PrefixName }}sRequest {StartPage = 1, PageSize = 4})).TotalElements;
        
        //Act
        var createRequest = new {{ PrefixName }}Dto { Name = Guid.NewGuid().ToString() };
        await _client.Create{{ PrefixName }}(createRequest);
        var response = await _client.Get{{ PrefixName }}s(new Get{{ PrefixName }}sRequest {StartPage = 1, PageSize = 4});
        
        //Assert
        
        Assert.Equal(beforeTotal + 1, response.TotalElements);
    }
    
    [Fact]
    public async Task Test_Get{{ PrefixName }}()
    {
        //Arrange
        var request = new {{ PrefixName }}Dto { Name = Guid.NewGuid().ToString() };
        var createResponse = await _client.Create{{ PrefixName }}(request);
    
        //Act
        var response = await _client.Get{{ PrefixName }}(new Get{{ PrefixName }}Request {Id = createResponse.{{ PrefixName }}.Id});
        
        //Assert
        var dto = response.{{ PrefixName }};
        Assert.NotNull(dto.Id);
        Assert.Equal(request.Name, dto.Name);
    }

    [Fact]
    public async Task Test_Update{{ PrefixName }}()
    {
        //Arrange
        var request = new {{ PrefixName }}Dto { Name = Guid.NewGuid().ToString() };
        var createResponse = await _client.Create{{ PrefixName }}(request);
    
        //Act
        var response = await _client.Update{{ PrefixName }}(new {{ PrefixName }}Dto() {Id = createResponse.{{ PrefixName }}.Id, Name = "Updated"});
        
        //Assert
        var dto = response.{{ PrefixName }};
        Assert.NotNull(dto.Id);
        Assert.Equal("Updated", response.{{ PrefixName }}.Name);
    }
    
    [Fact]
    public async Task Test_Delete{{ PrefixName }}()
    {
        //Arrange
        var request = new {{ PrefixName }}Dto { Name = Guid.NewGuid().ToString() };
        var createResponse = await _client.Create{{ PrefixName }}(request);
    
        //Act
        var response = await _client.Delete{{ PrefixName }}(new Delete{{ PrefixName }}Request{Id = createResponse.{{ PrefixName }}.Id});
        
        //Assert
        Assert.True(response.Deleted);
    }

    [Fact]
    public async Task Test_Delete{{ PrefixName }}_NotFound()
    {
        //Arrange

        //Act
        var exception = await Assert.ThrowsAsync<RpcException>(async () => 
        {
            await _client.Delete{{ PrefixName }}(new Delete{{ PrefixName }}Request{Id = Guid.NewGuid().ToString()});
        });
       
        //Assert
        Assert.Equal(StatusCode.NotFound, exception.StatusCode);
        Assert.Contains("{{ PrefixName }} with ID", exception.Status.Detail);
        Assert.Contains("was not found", exception.Status.Detail);
    }

}