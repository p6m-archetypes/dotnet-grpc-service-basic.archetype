#!/bin/bash
# Setup development environment for {{ prefix-name }}-{{ suffix-name }} .NET gRPC service

set -e

echo "🚀 Setting up {{ prefix-name }}-{{ suffix-name }} development environment..."

# Check if .NET SDK is installed
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET SDK not found. Please install .NET 8 SDK: https://dotnet.microsoft.com/download"
    exit 1
fi

# Check .NET version
DOTNET_VERSION=$(dotnet --version)
echo "✅ .NET SDK version: $DOTNET_VERSION"

# Check if Docker is installed
if ! command -v docker &> /dev/null; then
    echo "❌ Docker not found. Please install Docker: https://docs.docker.com/get-docker/"
    exit 1
fi

# Check if Docker Compose is available
if ! command -v docker-compose &> /dev/null; then
    echo "❌ Docker Compose not found. Please install Docker Compose"
    exit 1
fi

echo "✅ Docker and Docker Compose are available"

# Restore .NET packages
echo "📦 Restoring .NET packages..."
dotnet restore

# Install EF Core tools if not already installed
echo "🔧 Installing Entity Framework Core tools..."
dotnet tool install --global dotnet-ef --version 8.* || echo "EF Core tools already installed"

# Build the solution
echo "🔨 Building solution..."
dotnet build

# Create local development database
echo "🗃️ Setting up development database..."
docker-compose up -d postgres

# Wait for database to be ready
echo "⏳ Waiting for database to be ready..."
sleep 10

# Run database migrations
echo "🔄 Running database migrations..."
dotnet ef database update --project {{ PrefixName }}{{ SuffixName }}.Persistence --startup-project {{ PrefixName }}{{ SuffixName }}.Server

echo "✅ Development environment setup complete!"
echo ""
echo "🎯 Next steps:"
echo "  • Run tests: ./scripts/run-tests.sh"
echo "  • Start development server: ./scripts/start-dev.sh"
echo "  • Run integration tests: ./scripts/run-integration-tests.sh"
echo ""
