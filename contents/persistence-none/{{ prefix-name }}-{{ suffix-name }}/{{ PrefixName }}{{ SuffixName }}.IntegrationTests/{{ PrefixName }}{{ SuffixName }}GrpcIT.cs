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
        var createRequest = new {{ PrefixName }}Dto { Id = Guid.NewGuid().ToString(), Name = Guid.NewGuid().ToString() };
        var response = await _client.Create{{ PrefixName }}(createRequest);
        
        //Assert
        var dto = response.{{ PrefixName }};
        Assert.Equal(createRequest.Id, dto.Id);
        Assert.Equal(createRequest.Name, dto.Name);
    }
    
    [Fact]
    public async Task Test_Get{{ PrefixName }}s()
    {
        testOutputHelper.WriteLine("Test_Get{{ PrefixName }}s");
        
        //Arrange
        // This is a stub implementation that always returns empty results
        
        //Act
        var response = await _client.Get{{ PrefixName }}s(new Get{{ PrefixName }}sRequest {StartPage = 1, PageSize = 4});
        
        //Assert
        Assert.Equal(0, response.TotalElements);
        Assert.Equal(0, response.TotalPages);
        Assert.Empty(response.{{ PrefixName }}s);
    }
    
    [Fact]
    public async Task Test_Get{{ PrefixName }}()
    {
        //Arrange
        // This is a stub implementation that returns a fixed response
        var requestId = Guid.NewGuid().ToString();
    
        //Act
        var response = await _client.Get{{ PrefixName }}(new Get{{ PrefixName }}Request {Id = requestId});
        
        //Assert
        var dto = response.{{ PrefixName }};
        Assert.Equal("{{ PrefixName }}Id", dto.Id);
        Assert.Equal("{{ PrefixName }}Name", dto.Name);
    }

    [Fact]
    public async Task Test_Update{{ PrefixName }}()
    {
        //Arrange
        var updateRequest = new {{ PrefixName }}Dto { Id = Guid.NewGuid().ToString(), Name = "Updated" };
    
        //Act
        var response = await _client.Update{{ PrefixName }}(updateRequest);
        
        //Assert
        var dto = response.{{ PrefixName }};
        Assert.Equal(updateRequest.Id, dto.Id);
        Assert.Equal("Updated", dto.Name);
    }
    
    [Fact]
    public async Task Test_Delete{{ PrefixName }}()
    {
        //Arrange
        // This is a stub implementation that always returns false
        var deleteId = Guid.NewGuid().ToString();
    
        //Act
        var response = await _client.Delete{{ PrefixName }}(new Delete{{ PrefixName }}Request{Id = deleteId});
        
        //Assert
        Assert.False(response.Deleted);
    }

    [Fact]
    public async Task Test_Delete{{ PrefixName }}_StubBehavior()
    {
        //Arrange
        // This test verifies the stub always returns false (not deleted)
        // In a real implementation, this would throw NotFound for non-existent IDs

        //Act
        var response = await _client.Delete{{ PrefixName }}(new Delete{{ PrefixName }}Request{Id = Guid.NewGuid().ToString()});
       
        //Assert
        // Stub always returns false, doesn't throw exceptions
        Assert.False(response.Deleted);
    }

}
