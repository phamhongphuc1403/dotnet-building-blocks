# DOTNET-MICROSERVICES-BOILERPLATE

[![.NET](https://img.shields.io/badge/Framework-.NET-blue)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Container-Docker-blue)](https://www.docker.com/)
[![Redis](https://img.shields.io/badge/Cache-Redis-red)](https://redis.io/)
[![RabbitMQ](https://img.shields.io/badge/Messaging-RabbitMQ-orange)](https://www.rabbitmq.com/)
[![Serilog](https://img.shields.io/badge/Logging-Serilog-purple)](https://serilog.net/)

Built with the tools and technologies:
.NET, Docker, Redis, RabbitMQ, Serilog

---

## Table of Contents

- [Overview](#overview)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
- [Usage](#usage)

---

## Overview

The **dotnet-microservices-boilerplate** is a powerful starter kit designed to accelerate the development of scalable microservices applications using .NET.

### Why dotnet-microservices-boilerplate?

This project simplifies the creation and management of modular applications. The core features include:

- **Modular Architecture**: Promotes separation of concerns, making it easier to manage and scale applications.
- **Microservices Support**: Facilitates deployment and communication between services using Docker and RabbitMQ.
- **Caching with Redis**: Enhances performance by reducing redundant data fetching and improving application responsiveness.
- **Identity Management**: Provides robust user authentication and authorization features, ensuring secure access control.
- **Event-Driven Communication**: Utilizes an event bus for decoupled interactions between components, enhancing scalability.
- **Comprehensive Logging**: Integrates Serilog for structured logging, improving observability and debugging capabilities.

---

## Getting Started

### Prerequisites

This project requires the following dependencies:

- **Programming Language**: C#
- **Package Manager**: Nuget  
- **Container Runtime**: Docker  

---

## Usage

1. **Clone the repository**:
   ```bash
   git clone https://github.com/phuc1403/dotnet-microservices-boilerplate
   ```

2. **Run the project with**:
    ```bash
    docker-compose up
    ```
