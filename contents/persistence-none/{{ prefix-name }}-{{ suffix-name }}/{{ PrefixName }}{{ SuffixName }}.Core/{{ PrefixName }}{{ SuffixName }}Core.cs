using {{ PrefixName}}{{ SuffixName }}.API;
using {{ PrefixName}}{{ SuffixName }}.Core.Services;
using Microsoft.Extensions.Logging;

namespace {{ PrefixName}}{{ SuffixName }}.Core;

public class {{ PrefixName}}{{ SuffixName }}Core : I{{ PrefixName}}{{ SuffixName }}
{
    private readonly IValidationService _validationService;
    private readonly ILogger<{{ PrefixName}}{{ SuffixName }}Core> _logger;
       
    public {{ PrefixName}}{{ SuffixName }}Core(
        IValidationService validationService,
        ILogger<{{ PrefixName}}{{ SuffixName }}Core> logger) 
    {
        _validationService = validationService;
        _logger = logger;
    }

    public Task<CreateExampleResponse> CreateExample(ExampleDto request)
    {
          return Task.FromResult(new CreateExampleResponse
          {
              Example = new ExampleDto
              {
                  Id = request.Id,
                  Name = request.Name
              }
          });
    }

    public Task<GetExamplesResponse> GetExamples(GetExamplesRequest request)
    {
        return Task.FromResult(new GetExamplesResponse
        {
            TotalElements = 0,
            TotalPages = 0,
        });
    }

    public Task<GetExampleResponse> GetExample(GetExampleRequest request)
    { 
        return Task.FromResult(new GetExampleResponse
        {
            Example = new ExampleDto
            {
                Id = "ExampleId",
                Name = "ExampleName",
            }
        });
    }

    public Task<UpdateExampleResponse> UpdateExample(ExampleDto example)
    {
        return Task.FromResult(new UpdateExampleResponse
        {
            Example = new ExampleDto
            {
                Id = example.Id,
                Name = example.Name
            }
        });
    }

    public Task<DeleteExampleResponse> DeleteExample(DeleteExampleRequest request)
    {
        return Task.FromResult(new DeleteExampleResponse { Deleted = false });
    }
}
