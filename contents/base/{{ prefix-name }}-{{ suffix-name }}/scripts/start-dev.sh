#!/bin/bash
# Start {{ prefix-name }}-{{ suffix-name }} development server

set -e

echo "🚀 Starting {{ prefix-name }}-{{ suffix-name }} development server..."

# Start supporting services
echo "📦 Starting supporting services (PostgreSQL, Prometheus, Grafana)..."
docker-compose up -d postgres prometheus grafana

# Wait for services to be ready
echo "⏳ Waiting for services to be ready..."
sleep 5

# Check if database is responding
echo "🔍 Checking database connection..."
until docker-compose exec -T postgres pg_isready -U postgres -d {{ prefix_name }}_{{ suffix_name }}_db; do
  echo "⏳ Waiting for database..."
  sleep 2
done
echo "✅ Database is ready"

# Run database migrations
echo "🔄 Ensuring database schema is up to date..."
dotnet ef database update --project {{ PrefixName }}{{ SuffixName }}.Persistence --startup-project {{ PrefixName }}{{ SuffixName }}.Server

# Build the application
echo "🔨 Building application..."
dotnet build

echo "✅ Starting gRPC server..."
echo ""
echo "🎯 Server will be available at:"
echo "  • gRPC: http://localhost:{{ service-port }}"
echo "  • Management: http://localhost:{{ management-port }}"
echo "  • Health checks: http://localhost:{{ management-port }}/health"
echo "  • Metrics: http://localhost:{{ management-port }}/metrics"
echo ""
echo "📊 Monitoring available at:"
echo "  • Prometheus: http://localhost:9090"
echo "  • Grafana: http://localhost:3000 (admin/admin)"
echo ""
echo "🛑 Press Ctrl+C to stop the server"
echo ""

# Start the server
dotnet run --project {{ PrefixName }}{{ SuffixName }}.Server
