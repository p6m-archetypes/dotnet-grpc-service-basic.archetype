#!/bin/bash
# Run integration tests for {{ prefix-name }}-{{ suffix-name }} .NET gRPC service

set -e

echo "🔗 Running {{ prefix-name }}-{{ suffix-name }} integration tests..."

# Start all required services
echo "📦 Starting required services..."
docker-compose up -d postgres

# Wait for services to be ready
echo "⏳ Waiting for services to be ready..."
until docker-compose exec -T postgres pg_isready -U postgres -d {{ prefix_name }}_{{ suffix_name }}_db; do
  echo "⏳ Waiting for database..."
  sleep 2
done

echo "✅ Services are ready"

# Ensure database schema is current
echo "🔄 Updating database schema..."
dotnet ef database update --project {{ PrefixName }}{{ SuffixName }}.Persistence --startup-project {{ PrefixName }}{{ SuffixName }}.Server

# Build the solution
echo "🔨 Building solution..."
dotnet build

# Run integration tests with detailed output
echo "🧪 Running integration tests..."
dotnet test {{ PrefixName }}{{ SuffixName }}.IntegrationTests \
    --configuration Debug \
    --logger "console;verbosity=detailed" \
    --collect:"XPlat Code Coverage" \
    --results-directory ./TestResults/IntegrationTests \
    --filter "Category=Integration"

# Check test results
if [ $? -eq 0 ]; then
    echo ""
    echo "✅ Integration tests passed!"
    echo "📊 Test results saved to ./TestResults/IntegrationTests/"
else
    echo ""
    echo "❌ Integration tests failed!"
    echo "📋 Check test output above for details"
    exit 1
fi

echo ""
echo "🔧 Integration test environment is still running."
echo "🌐 You can access:"
echo "  • Database: localhost:5432"
echo "  • Prometheus: http://localhost:9090"
echo "  • Grafana: http://localhost:3000"
echo ""
echo "🛑 Run 'docker-compose down' to stop all services"
