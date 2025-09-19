using {{ PrefixName }}{{ SuffixName }}.Client;
using {{ PrefixName }}{{ SuffixName }}.Server;

namespace {{ PrefixName }}{{ SuffixName }}.IntegrationTests;

public class ApplicationFixture: IDisposable
{
    private readonly {{ PrefixName }}{{ SuffixName }}Server _server;
    private readonly {{ PrefixName }}{{ SuffixName }}Client _client;
    public ApplicationFixture()
    {
        _server = new {{ PrefixName }}{{ SuffixName }}Server()
            .WithEphemeral()
            .WithRandomPorts()
            .Start();
        
        var grpcUrl = _server.getGrpcUrl();
        if (string.IsNullOrEmpty(grpcUrl))
        {
            throw new InvalidOperationException("Failed to get gRPC server URL");
        }
        
        _client = {{ PrefixName }}{{ SuffixName }}Client.Of(grpcUrl);
    }
    
    public {{ PrefixName }}{{ SuffixName }}Client GetClient() => _client;
    public {{ PrefixName }}{{ SuffixName }}Server GetServer() => _server;

    public void Dispose()
    {
        _server.Stop();
    }
}

[CollectionDefinition("ApplicationCollection")]
public class ApplicationCollection : ICollectionFixture<ApplicationFixture>
{
    // This class has no code; it's just a marker for the test collection
}