#!/bin/bash
# Run tests for {{ prefix-name }}-{{ suffix-name }} .NET gRPC service

set -e

echo "🧪 Running {{ prefix-name }}-{{ suffix-name }} tests..."

# Build the solution first
echo "🔨 Building solution..."
dotnet build

# Run unit tests
echo "🧪 Running unit tests..."
dotnet test {{ PrefixName }}{{ SuffixName }}.UnitTests \
    --configuration Debug \
    --logger "console;verbosity=detailed" \
    --collect:"XPlat Code Coverage" \
    --results-directory ./TestResults/UnitTests

# Check if unit tests passed
if [ $? -ne 0 ]; then
    echo "❌ Unit tests failed"
    exit 1
fi

echo "✅ Unit tests passed"

# Start test database for integration tests
echo "🗃️ Starting test database..."
docker-compose up -d postgres

# Wait for database
echo "⏳ Waiting for database..."
until docker-compose exec -T postgres pg_isready -U postgres -d {{ prefix_name }}_{{ suffix_name }}_db; do
  sleep 2
done

# Run database migrations for testing
echo "🔄 Setting up test database..."
dotnet ef database update --project {{ PrefixName }}{{ SuffixName }}.Persistence --startup-project {{ PrefixName }}{{ SuffixName }}.Server

# Run integration tests
echo "🔗 Running integration tests..."
dotnet test {{ PrefixName }}{{ SuffixName }}.IntegrationTests \
    --configuration Debug \
    --logger "console;verbosity=detailed" \
    --collect:"XPlat Code Coverage" \
    --results-directory ./TestResults/IntegrationTests

# Check if integration tests passed
if [ $? -ne 0 ]; then
    echo "❌ Integration tests failed"
    exit 1
fi

echo "✅ Integration tests passed"

# Generate combined test report
echo "📊 Generating test coverage report..."
echo "Test results saved to ./TestResults/"

echo ""
echo "🎉 All tests passed successfully!"
echo "📊 Coverage reports available in ./TestResults/"
