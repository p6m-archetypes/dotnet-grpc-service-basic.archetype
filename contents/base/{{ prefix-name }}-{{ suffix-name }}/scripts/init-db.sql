-- Initialize {{ prefix-name }}-{{ suffix-name }} Service database
-- This script runs during container startup for .NET gRPC service

-- Create database if it doesn't exist (handled by POSTGRES_DB env var)
-- CREATE DATABASE {{ prefix_name }}_{{ suffix_name }}_db;

-- Switch to the database
\c {{ prefix_name }}_{{ suffix_name }}_db;

-- Create extensions if needed
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Set default timezone
SET timezone = 'UTC';

-- Create example table (this will be handled by Entity Framework migrations in production)
-- This is here for development/testing purposes
CREATE TABLE IF NOT EXISTS {{ prefix_name }}_entities (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(255) NOT NULL,
    description TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    modified_at TIMESTAMP WITH TIME ZONE,
    version INTEGER DEFAULT 1
);

-- Create indexes
CREATE INDEX IF NOT EXISTS idx_{{ prefix_name }}_entities_name ON {{ prefix_name }}_entities(name);
CREATE INDEX IF NOT EXISTS idx_{{ prefix_name }}_entities_created_at ON {{ prefix_name }}_entities(created_at);

-- Insert sample data for development
INSERT INTO {{ prefix_name }}_entities (name, description) VALUES 
    ('Sample {{ PrefixName }} 1', 'First sample entity for development'),
    ('Sample {{ PrefixName }} 2', 'Second sample entity for development'),
    ('Sample {{ PrefixName }} 3', 'Third sample entity for development')
ON CONFLICT DO NOTHING;

-- Grant permissions (adjust as needed for production)
-- GRANT ALL PRIVILEGES ON DATABASE {{ prefix_name }}_{{ suffix_name }}_db TO postgres;
-- GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO postgres;
-- GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO postgres;
