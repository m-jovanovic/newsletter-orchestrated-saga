# Orchestrated Saga Example

Orchestrated Saga example with:
- Rebus
- RabbitMQ
- SQL Server

Showcasing how to create a saga with multiple steps, and even deferring messages to the future.

## Running this project

Running should be easy enough since I added orchestration with `docker compose`.

However, you'll need to manually connect to the SQL Server instance in the container to create the database.

Default database name is `Newsletter`.
