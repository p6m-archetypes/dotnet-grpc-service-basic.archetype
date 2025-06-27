
namespace {{ PrefixName }}{{ SuffixName }}.API;

public interface I{{ PrefixName }}{{ SuffixName }}
{
    Task<Create{{ PrefixName }}Response> Create{{ PrefixName }}({{ PrefixName }}Dto {{ prefixName }});
    Task<Get{{ PrefixName }}sResponse> Get{{ PrefixName }}s(Get{{ PrefixName }}sRequest request);
    Task<Get{{ PrefixName }}Response> Get{{ PrefixName }}(Get{{ PrefixName }}Request request);
    Task<Update{{ PrefixName }}Response> Update{{ PrefixName }}({{ PrefixName }}Dto {{ prefixName }});
    Task<Delete{{ PrefixName }}Response> Delete{{ PrefixName }}(Delete{{ PrefixName }}Request request);
    
}