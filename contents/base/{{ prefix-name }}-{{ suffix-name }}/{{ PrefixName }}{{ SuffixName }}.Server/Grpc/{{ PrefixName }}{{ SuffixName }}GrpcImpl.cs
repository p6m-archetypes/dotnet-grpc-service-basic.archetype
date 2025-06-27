using Grpc.Core;
using {{ PrefixName }}{{ SuffixName }}.API;
using {{ PrefixName }}{{ SuffixName }}.Core;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace {{ PrefixName }}{{ SuffixName }}.Server.Grpc;

public class {{ PrefixName }}{{ SuffixName }}GrpcImpl : API.{{ PrefixName }}{{ SuffixName }}.{{ PrefixName }}{{ SuffixName }}Base
{
    private readonly ILogger<{{ PrefixName }}{{ SuffixName }}GrpcImpl> _logger;
    private readonly {{ PrefixName }}{{ SuffixName }}Core _service;
    
    public {{ PrefixName }}{{ SuffixName }}GrpcImpl({{ PrefixName }}{{ SuffixName }}Core service, ILogger<{{ PrefixName }}{{ SuffixName }}GrpcImpl> logger)
    {
        _service = service;
        _logger = logger;
    }

    public override async Task<Create{{ PrefixName }}Response> Create{{ PrefixName }}({{ PrefixName }}Dto request, ServerCallContext context)
    {
        using var scope = _logger.BeginScope("gRPC: {Method}, User: {UserId}", 
            nameof(Create{{ PrefixName }}), GetUserId(context));
            
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            _logger.LogDebug("gRPC Create{{ PrefixName }} started for {Name}", request.Name);
            
            var response = await _service.Create{{ PrefixName }}(request);
            
            stopwatch.Stop();
            _logger.LogInformation("gRPC Create{{ PrefixName }} completed successfully in {Duration}ms", 
                stopwatch.ElapsedMilliseconds);
                
            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "gRPC Create{{ PrefixName }} failed after {Duration}ms", 
                stopwatch.ElapsedMilliseconds);
            throw;
        }
    }

    public override async Task<Get{{ PrefixName }}Response> Get{{ PrefixName }}(Get{{ PrefixName }}Request request, ServerCallContext context)
    {
        using var scope = _logger.BeginScope("gRPC: {Method}, User: {UserId}, Id: {Id}", 
            nameof(Get{{ PrefixName }}), GetUserId(context), request.Id);
            
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            _logger.LogDebug("gRPC Get{{ PrefixName }} started for ID {Id}", request.Id);
            
            var response = await _service.Get{{ PrefixName }}(request);
            
            stopwatch.Stop();
            _logger.LogInformation("gRPC Get{{ PrefixName }} completed successfully in {Duration}ms", 
                stopwatch.ElapsedMilliseconds);
                
            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "gRPC Get{{ PrefixName }} failed for ID {Id} after {Duration}ms", 
                request.Id, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }

    public override async Task<Get{{ PrefixName }}sResponse> Get{{ PrefixName }}s(Get{{ PrefixName }}sRequest request, ServerCallContext context)
    {
        using var scope = _logger.BeginScope("gRPC: {Method}, User: {UserId}, Page: {StartPage}, Size: {PageSize}", 
            nameof(Get{{ PrefixName }}s), GetUserId(context), request.StartPage, request.PageSize);
            
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            _logger.LogDebug("gRPC Get{{ PrefixName }}s started for page {StartPage}, size {PageSize}", 
                request.StartPage, request.PageSize);
            
            var response = await _service.Get{{ PrefixName }}s(request);
            
            stopwatch.Stop();
            _logger.LogInformation("gRPC Get{{ PrefixName }}s completed successfully in {Duration}ms - returned {Count}/{Total} items", 
                stopwatch.ElapsedMilliseconds, response.{{ PrefixName }}s.Count, response.TotalElements);
                
            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "gRPC Get{{ PrefixName }}s failed for page {StartPage}, size {PageSize} after {Duration}ms", 
                request.StartPage, request.PageSize, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }

    public override async Task<Update{{ PrefixName }}Response> Update{{ PrefixName }}({{ PrefixName }}Dto request, ServerCallContext context)
    {
        using var scope = _logger.BeginScope("gRPC: {Method}, User: {UserId}, Id: {Id}", 
            nameof(Update{{ PrefixName }}), GetUserId(context), request.Id);
            
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            _logger.LogDebug("gRPC Update{{ PrefixName }} started for ID {Id}", request.Id);
            
            var response = await _service.Update{{ PrefixName }}(request);
            
            stopwatch.Stop();
            _logger.LogInformation("gRPC Update{{ PrefixName }} completed successfully in {Duration}ms", 
                stopwatch.ElapsedMilliseconds);
                
            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "gRPC Update{{ PrefixName }} failed for ID {Id} after {Duration}ms", 
                request.Id, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }

    public override async Task<Delete{{ PrefixName }}Response> Delete{{ PrefixName }}(Delete{{ PrefixName }}Request request, ServerCallContext context)
    {
        using var scope = _logger.BeginScope("gRPC: {Method}, User: {UserId}, Id: {Id}", 
            nameof(Delete{{ PrefixName }}), GetUserId(context), request.Id);
            
        var stopwatch = Stopwatch.StartNew();
        
        try
        {
            _logger.LogDebug("gRPC Delete{{ PrefixName }} started for ID {Id}", request.Id);
            
            var response = await _service.Delete{{ PrefixName }}(request);
            
            stopwatch.Stop();
            _logger.LogInformation("gRPC Delete{{ PrefixName }} completed successfully in {Duration}ms - deleted: {Deleted}", 
                stopwatch.ElapsedMilliseconds, response.Deleted);
                
            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "gRPC Delete{{ PrefixName }} failed for ID {Id} after {Duration}ms", 
                request.Id, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
    
    /// <summary>
    /// Extracts user ID from gRPC context metadata.
    /// </summary>
    private static string? GetUserId(ServerCallContext context)
    {
        // Try to get user ID from various possible metadata entries
        var userIdEntry = context.RequestHeaders.FirstOrDefault(h => 
            string.Equals(h.Key, "user-id", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(h.Key, "x-user-id", StringComparison.OrdinalIgnoreCase));
        
        return userIdEntry?.Value;
    }
}