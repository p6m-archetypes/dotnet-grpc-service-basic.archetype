#!/bin/bash
# Build script for {{ prefix-name }}-{{ suffix-name }} .NET gRPC service
# Cross-platform build hook with automatic gRPC code generation

set -e

# Default values
CONFIGURATION="Release"
SKIP_TESTS=false
SKIP_RESTORE=false
VERBOSE=false

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        --configuration|-c)
            CONFIGURATION="$2"
            shift 2
            ;;
        --skip-tests)
            SKIP_TESTS=true
            shift
            ;;
        --skip-restore)
            SKIP_RESTORE=true
            shift
            ;;
        --verbose|-v)
            VERBOSE=true
            shift
            ;;
        *)
            echo "Unknown option: $1"
            exit 1
            ;;
    esac
done

# Set verbosity
if [ "$VERBOSE" = true ]; then
    VERBOSITY="detailed"
else
    VERBOSITY="minimal"
fi

echo "🚀 Building {{ prefix-name }}-{{ suffix-name }} .NET gRPC service..."

# Step 1: Restore packages
if [ "$SKIP_RESTORE" = false ]; then
    echo "📦 Restoring NuGet packages..."
    dotnet restore --verbosity $VERBOSITY
fi

# Step 2: Generate gRPC code (if proto files exist)
PROTO_FILES=$(find . -name "*.proto" -type f)
if [ -n "$PROTO_FILES" ]; then
    echo "🔄 Generating gRPC code from proto files..."
    
    for proto_file in $PROTO_FILES; do
        echo "  Processing: $(basename $proto_file)"
    done
    
    # Build will automatically generate gRPC code via MSBuild targets
    echo "✅ gRPC code generation will be handled by MSBuild"
fi

# Step 3: Build solution
echo "🔨 Building solution ($CONFIGURATION)..."
dotnet build --configuration $CONFIGURATION --no-restore --verbosity $VERBOSITY

# Step 4: Run tests (unless skipped)
if [ "$SKIP_TESTS" = false ]; then
    echo "🧪 Running tests..."
    
    # Run unit tests
    dotnet test {{ PrefixName }}{{ SuffixName }}.UnitTests --configuration $CONFIGURATION --no-build --verbosity $VERBOSITY
    
    # Note: Integration tests require external dependencies, so they're separate
    echo "✅ Unit tests passed. Run integration tests separately with run-integration-tests.sh"
fi

# Step 5: Package (for Release builds)
if [ "$CONFIGURATION" = "Release" ]; then
    echo "📦 Creating packages..."
    dotnet pack --configuration $CONFIGURATION --no-build --verbosity $VERBOSITY
fi

echo ""
echo "🎉 Build completed successfully!"
echo "📋 Build artifacts:"
echo "  • Binaries: bin/$CONFIGURATION/"
if [ "$CONFIGURATION" = "Release" ]; then
    echo "  • Packages: bin/$CONFIGURATION/*.nupkg"
fi
echo ""
echo "🎯 Next steps:"
echo "  • Run server: dotnet run --project {{ PrefixName }}{{ SuffixName }}.Server"
echo "  • Run tests: ./scripts/run-tests.sh"  
echo "  • Integration tests: ./scripts/run-integration-tests.sh"
