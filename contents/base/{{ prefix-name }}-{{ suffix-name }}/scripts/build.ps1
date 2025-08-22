# Build script for {{ prefix-name }}-{{ suffix-name }} .NET gRPC service
# PowerShell build hook with automatic gRPC code generation

param(
    [string]$Configuration = "Release",
    [switch]$SkipTests = $false,
    [switch]$SkipRestore = $false,
    [switch]$Verbose = $false
)

$ErrorActionPreference = "Stop"

Write-Host "🚀 Building {{ prefix-name }}-{{ suffix-name }} .NET gRPC service..." -ForegroundColor Green

# Set verbosity
$VerbosityLevel = if ($Verbose) { "detailed" } else { "minimal" }

try {
    # Step 1: Restore packages
    if (-not $SkipRestore) {
        Write-Host "📦 Restoring NuGet packages..." -ForegroundColor Yellow
        dotnet restore --verbosity $VerbosityLevel
        if ($LASTEXITCODE -ne 0) {
            throw "Package restore failed"
        }
    }

    # Step 2: Generate gRPC code (if proto files exist)
    $ProtoFiles = Get-ChildItem -Path "." -Filter "*.proto" -Recurse
    if ($ProtoFiles.Count -gt 0) {
        Write-Host "🔄 Generating gRPC code from proto files..." -ForegroundColor Yellow
        
        foreach ($ProtoFile in $ProtoFiles) {
            Write-Host "  Processing: $($ProtoFile.Name)" -ForegroundColor Gray
        }
        
        # Build will automatically generate gRPC code via MSBuild targets
        Write-Host "✅ gRPC code generation will be handled by MSBuild" -ForegroundColor Green
    }

    # Step 3: Build solution
    Write-Host "🔨 Building solution ($Configuration)..." -ForegroundColor Yellow
    dotnet build --configuration $Configuration --no-restore --verbosity $VerbosityLevel
    if ($LASTEXITCODE -ne 0) {
        throw "Build failed"
    }

    # Step 4: Run tests (unless skipped)
    if (-not $SkipTests) {
        Write-Host "🧪 Running tests..." -ForegroundColor Yellow
        
        # Run unit tests
        dotnet test {{ PrefixName }}{{ SuffixName }}.UnitTests --configuration $Configuration --no-build --verbosity $VerbosityLevel
        if ($LASTEXITCODE -ne 0) {
            throw "Unit tests failed"
        }
        
        # Note: Integration tests require external dependencies, so they're separate
        Write-Host "✅ Unit tests passed. Run integration tests separately with run-integration-tests.sh" -ForegroundColor Green
    }

    # Step 5: Package (for Release builds)
    if ($Configuration -eq "Release") {
        Write-Host "📦 Creating packages..." -ForegroundColor Yellow
        dotnet pack --configuration $Configuration --no-build --verbosity $VerbosityLevel
        if ($LASTEXITCODE -ne 0) {
            throw "Packaging failed"
        }
    }

    Write-Host ""
    Write-Host "🎉 Build completed successfully!" -ForegroundColor Green
    Write-Host "📋 Build artifacts:" -ForegroundColor White
    Write-Host "  • Binaries: bin/$Configuration/" -ForegroundColor Gray
    if ($Configuration -eq "Release") {
        Write-Host "  • Packages: bin/$Configuration/*.nupkg" -ForegroundColor Gray
    }
    Write-Host ""
    Write-Host "🎯 Next steps:" -ForegroundColor White
    Write-Host "  • Run server: dotnet run --project {{ PrefixName }}{{ SuffixName }}.Server" -ForegroundColor Gray
    Write-Host "  • Run tests: ./scripts/run-tests.sh" -ForegroundColor Gray
    Write-Host "  • Integration tests: ./scripts/run-integration-tests.sh" -ForegroundColor Gray

} catch {
    Write-Host ""
    Write-Host "❌ Build failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}
