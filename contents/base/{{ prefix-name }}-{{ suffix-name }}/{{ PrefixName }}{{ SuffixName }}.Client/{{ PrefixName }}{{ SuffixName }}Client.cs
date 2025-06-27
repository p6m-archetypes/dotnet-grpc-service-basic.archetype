using Grpc.Net.Client;
using {{ PrefixName }}{{ SuffixName }}.API;

namespace {{ PrefixName }}{{ SuffixName }}.Client;

public class {{ PrefixName }}{{ SuffixName }}Client : I{{ PrefixName }}{{ SuffixName }}
{
    private readonly API.{{ PrefixName }}{{ SuffixName }}.{{ PrefixName }}{{ SuffixName }}Client stub;

    private {{ PrefixName }}{{ SuffixName }}Client(GrpcChannel channel)
    {
        stub = new API.{{ PrefixName }}{{ SuffixName }}.{{ PrefixName }}{{ SuffixName }}Client(channel);
    }

    public static {{ PrefixName }}{{ SuffixName }}Client Of(string host)
    {
        return new {{ PrefixName }}{{ SuffixName }}Client(GrpcChannel.ForAddress(host));
    }

    public async Task<Create{{ PrefixName }}Response> Create{{ PrefixName }}({{ PrefixName }}Dto {{ prefixName }}) {
        return await stub.Create{{ PrefixName }}Async({{ prefixName }});
    }

    public async Task<Get{{ PrefixName }}sResponse> Get{{ PrefixName }}s(Get{{ PrefixName }}sRequest request) {
        return await stub.Get{{ PrefixName }}sAsync(request);
    }

    public async Task<Get{{ PrefixName }}Response> Get{{ PrefixName }}(Get{{ PrefixName }}Request request) {
        return await stub.Get{{ PrefixName }}Async(request);
    }

    public async Task<Update{{ PrefixName }}Response> Update{{ PrefixName }}({{ PrefixName }}Dto {{ prefixName }}) {
        return await stub.Update{{ PrefixName }}Async({{ prefixName }});
    }

    public async Task<Delete{{ PrefixName }}Response> Delete{{ PrefixName }}(Delete{{ PrefixName }}Request request) {
        return await stub.Delete{{ PrefixName }}Async(request);
    }
    

}