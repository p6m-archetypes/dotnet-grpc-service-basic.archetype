# .NET gRPC Service Archetype

![Latest Release](https://img.shields.io/github/v/release/p6m-archetypes/dotnet-grpc-service-basic.archetype?style=flat-square&label=Latest%20Release&color=blue)

Production-ready archetype for generating modular .NET gRPC microservices with Entity Framework Core, flexible persistence options, and modern observability.

## 🎯 What This Generates

This archetype creates a complete, production-ready gRPC service with:

- **🏗️ Modular Architecture**: Namespace-organized, service-oriented design with separate API, Core, and Persistence layers
- **⚡ Modern .NET Stack**: .NET 8+ with Entity Framework Core
- **🔌 gRPC Communication**: Type-safe gRPC APIs with automatic client stub generation and publication
- **💾 Flexible Persistence**: Choose from PostgreSQL, MySQL, MSSQL, or no database
- **🐳 Container-Ready**: Docker and Kubernetes deployment manifests
- **📊 Built-in Monitoring**: Health checks and metrics endpoints
- **🧪 Comprehensive Testing**: Unit and integration tests with Testcontainers
- **⚡ Load Testing**: k6 performance tests for both HTTP and gRPC calls
- **🔄 CI/CD Pipeline**: Artifact publication to Artifactory
- **🔧 Local Development**: Tilt integration for Kubernetes development

## 📦 Generated Project Structure

```
my-shopping-cart-service/
├── src/
│   ├── ShoppingCart.Api/          # gRPC service definitions (.proto files)
│   ├── ShoppingCart.Client/       # gRPC client stubs (published package)
│   ├── ShoppingCart.Core/         # Business logic and domain models
│   ├── ShoppingCart.Persistence/  # Entity Framework data layer
│   └── ShoppingCart.Server/       # gRPC server implementation
├── tests/
│   ├── ShoppingCart.UnitTests/
│   └── ShoppingCart.IntegrationTests/
├── k6/                            # Load testing scripts
├── k8s/                            # Kubernetes manifests
├── Dockerfile
└── docker-compose.yml
```

## 🚀 Quick Start

### Prerequisites

- [Archetect](https://archetect.github.io/) CLI tool
- .NET 8 SDK or later
- Docker Desktop (for containerized development and testing)

### Generate a New Service

```bash
# Using SSH
archetect render git@github.com:p6m-archetypes/dotnet-grpc-service-basic.archetype.git

# Using HTTPS
archetect render https://github.com/p6m-archetypes/dotnet-grpc-service-basic.archetype.git

# Example prompt answers:
# project: Shopping Cart
# suffix: Service
# group-prefix: com.example
# team-name: Platform
# persistence: PostgreSQL
# service-port: 50051
```

### Development Workflow

```bash
cd shopping-cart-service

# 1. Restore dependencies
dotnet restore

# 2. Run tests
dotnet test

# 3. Start the service
dotnet run --project src/ShoppingCart.Server

# 4. Access endpoints
# - gRPC Service: localhost:50051
# - Health Check: http://localhost:50052/health
# - Health Live: http://localhost:50052/health/live
# - Health Ready: http://localhost:50052/health/ready
# - Metrics: http://localhost:50052/metrics
```

## 📋 Configuration Prompts

When rendering the archetype, you'll be prompted for the following values:

| Property | Description | Example | Required |
|----------|-------------|---------|----------|
| `project` | Service domain name used for namespaces, entities, and RPC stub names | Shopping Cart | Yes |
| `suffix` | Appended to project name for package naming | Service | Yes |
| `group-prefix` | Namespace prefix (reverse domain notation) | com.example | Yes |
| `team-name` | Owning team identifier for artifacts and documentation | Platform | Yes |
| `persistence` | Database type for data persistence | PostgreSQL | Yes |
| `service-port` | Port for gRPC traffic | 50051 | Yes |

**Derived Properties:**
- `management-port`: Automatically set to `service-port + 1` for health/metrics HTTP endpoints
- `database-port`: Set to 5432 for PostgreSQL-based services
- `debug-port`: Set to `service-port + 9` for debugging

For complete property relationships, see [archetype.yaml](./archetype.yaml).

## ✨ Key Features

### 🏛️ Architecture & Design

- **Modular Structure**: Separation of API, Client, Core, Persistence, and Server concerns
- **Domain-Driven Design**: Entity-centric business logic organization
- **Dependency Injection**: Built-in .NET DI container configuration
- **Clean Architecture**: Dependencies flow toward domain core
- **Contract-First Design**: Protocol Buffer definitions drive implementation

### 🔧 Technology Stack

- **.NET 8+**: Latest LTS framework with performance improvements
- **gRPC**: High-performance RPC framework with HTTP/2
- **Protocol Buffers**: Efficient binary serialization
- **Entity Framework Core**: Modern ORM with migration support and async operations
- **Testcontainers**: Containerized integration testing with real databases
- **k6**: High-performance load testing for both HTTP and gRPC
- **Tilt**: Local Kubernetes development workflow with hot reload

### 📊 Observability & Monitoring

- **Health Checks**: Liveness and readiness endpoints for Kubernetes probes
- **Metrics**: Prometheus-compatible metrics endpoint
- **Structured Logging**: Configurable log levels with structured output
- **Request Tracing**: Distributed tracing support (OpenTelemetry-ready)
- **gRPC Metrics**: Call performance tracking and monitoring

### 🧪 Testing & Quality

- **Unit Tests**: xUnit test projects for business logic validation
- **Integration Tests**: Full service testing with Testcontainers and real databases
- **gRPC Testing**: Service method testing with test client
- **Load Tests**: k6 scripts for gRPC performance and stress testing
- **Test Coverage**: Configured coverage reporting

### 🚢 DevOps & Deployment

- **Docker**: Multi-stage Dockerfile for optimized production images
- **Kubernetes**: Complete deployment manifests with ConfigMaps and Secrets
- **Tilt**: Hot-reload development in local Kubernetes clusters
- **Artifactory**: Docker image and gRPC stub package publication
- **Health Probes**: Kubernetes liveness and readiness probe configuration

## 🎯 Use Cases

This archetype is ideal for:

1. **Backend Microservices**: Building internal services with high-performance RPC communication
2. **Data-Driven Services**: Services requiring relational database persistence with ORM
3. **Platform Services**: Core platform services deployed in Kubernetes environments
4. **Inter-Service Communication**: Services requiring efficient, type-safe communication between microservices

## 📚 What's Inside

### Core Components

#### gRPC Service Definition
Protocol Buffer definitions (.proto files) with CRUD operations. Supports automatic code generation for multiple programming languages, enabling polyglot client development.

#### Client Stub Package
Pre-built client library package (.Client project) ready for publication to Artifactory. Consumers can add this package to their projects for type-safe gRPC communication.

#### Entity Framework Persistence
Database access layer with migrations, connection pooling, and async operations. Supports PostgreSQL, MySQL, MSSQL, or no persistence for stateless services.

#### Health & Metrics
Built-in health check endpoints for Kubernetes liveness/readiness probes and Prometheus metrics for monitoring call performance and resource usage.

### Development Tools

- **Tilt Configuration**: Auto-reload development in Kubernetes with live updates
- **Docker Compose**: Local development stack with database services
- **k6 Load Tests**: Performance testing scripts for both HTTP and gRPC endpoints
- **grpcurl**: Command-line tool support for testing gRPC services

### Configuration Management

- **appsettings.json**: Environment-specific configuration files
- **Environment Variables**: 12-factor app configuration support
- **CLI Arguments**: Runtime configuration overrides
- **Connection Strings**: Secure database connection management

## 🔧 gRPC-Specific Features

### Service Definition

- **Protocol Buffers**: Type-safe service contracts with strong typing
- **Code Generation**: Automatic client and server stub generation
- **Multi-Language Support**: Generate clients for Java, Python, Go, Node.js, and more
- **Versioning**: Proto file versioning strategy with backward compatibility
- **Documentation**: In-proto documentation comments that generate to client libraries

### Client Stub Publication

- **NuGet Package**: Pre-built client library ready for Artifactory publication
- **Type Safety**: Strongly-typed request/response objects
- **Async/Await**: Fully asynchronous client API
- **Connection Management**: Built-in connection pooling and retry logic
- **Easy Integration**: Simple package reference for consuming services

### Advanced Features

- **Streaming**: Support for server streaming, client streaming, and bidirectional streaming
- **Interceptors**: Request/response middleware for authentication, logging, and metrics
- **Reflection**: gRPC reflection service for dynamic client tools
- **Health Checking**: Standard gRPC health checking protocol
- **Deadlines/Timeouts**: Built-in timeout and deadline propagation
- **Channel Options**: Configurable keepalive, message size, and compression

### Performance

- **HTTP/2**: Multiplexing multiple requests over single connection
- **Binary Protocol**: Efficient Protocol Buffer serialization
- **Connection Pooling**: Reusable connections for reduced latency
- **Compression**: Optional gzip compression for reduced bandwidth
- **Load Balancing**: Client-side and server-side load balancing support

## 📋 Validation & Quality Assurance

Generated services are production-ready and include:

- ✅ Successful .NET build and compilation
- ✅ All unit and integration tests pass
- ✅ Docker image builds successfully
- ✅ Service starts and responds to health checks
- ✅ gRPC service accepts and responds to calls
- ✅ Database migrations execute successfully
- ✅ Client stub package builds successfully

Validate your generated service:

```bash
dotnet build
dotnet test
docker build -t my-service .
```

### Manual Testing with grpcurl

Test gRPC endpoints using grpcurl:

```bash
# List available services
grpcurl -plaintext localhost:50051 list

# Describe a service
grpcurl -plaintext localhost:50051 describe shopping_cart.v1.ShoppingCartService

# Call a method
grpcurl -plaintext -d '{"id": "123"}' \
  localhost:50051 shopping_cart.v1.ShoppingCartService/Get
```

### Load Testing

Run k6 performance tests for gRPC:

```bash
k6 run k6/grpc-load-test.js
```

## 🛠️ Advanced Features

### Stub Publication

Publish the gRPC client stub to Artifactory for consumption by other teams:

```bash
# Build the client package
dotnet pack src/ShoppingCart.Client

# Publish to Artifactory
dotnet nuget push src/ShoppingCart.Client/bin/Release/*.nupkg \
  --source https://p6m.jfrog.io/artifactory/api/nuget/nuget-local
```

### Consuming the Client Stub

Other services can consume your gRPC service by adding the client package:

```bash
dotnet add package ShoppingCart.Client
```

Then use it in code:

```csharp
using var channel = GrpcChannel.ForAddress("https://shopping-cart-service:50051");
var client = new ShoppingCartService.ShoppingCartServiceClient(channel);
var response = await client.GetAsync(new GetRequest { Id = "123" });
```

## 🔗 Related Archetypes

- **[.NET REST Service](../dotnet-rest-service-basic.archetype)** - For HTTP REST APIs instead of gRPC
- **[.NET GraphQL Service](../dotnet-graphql-service-basic.archetype)** - For flexible GraphQL APIs
- **[Python gRPC Service](../python-grpc-service-uv.archetype)** - Python alternative with modern tooling
- **[Java Spring Boot gRPC Service](../java-spring-boot-grpc-service.archetype)** - Java alternative with Spring Boot

## 🤝 Contributing

This archetype is actively maintained. For issues or enhancements:

1. Check existing issues in the repository
2. Create detailed bug reports or feature requests
3. Follow the contribution guidelines
4. Test changes with the validation suite
5. Verify client stub generation and publication

## 📄 License

This archetype is released under the MIT License. Generated services inherit this license but can be changed as needed for your organization.

---

**Ready to build production-grade gRPC services with .NET?** Generate your first service and start building in minutes! 🚀
