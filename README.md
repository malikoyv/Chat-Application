# Simple Chat Application

This project implements a simple chat application using C# and ASP.NET Core, designed to allow users to create, search for, connect to, and delete chats. The application utilizes ASP.NET Core Web API for RESTful endpoints, Entity Framework Core for database operations using a Code First approach, and SignalR for real-time communication between clients.

## Tech Stack

- **ASP.NET Core (Web API)**: Used for building RESTful APIs.
- **Entity Framework Core (Code First)**: Provides data access and persistence.
- **SignalR**: Enables real-time web functionality for chat features.
- **SQL Server**: Database used for storing users, chats, and messages.
- **xUnit**: A testing framework for unit and integration tests.
- **Moq**: Mocking library used for testing.

## Architecture

The application follows a 3-tier architecture:

- **Presentation Layer**: Exposes RESTful APIs for clients to interact with.
- **Business Logic Layer**: Contains services to handle business rules and workflows.
- **Data Access Layer**: Uses Entity Framework Core to interact with the database.

## Components

- **Controllers**: Implement CRUD operations for users, chats, and messages.
- **SignalR Hub**: Handles real-time communication between clients for chat messages.
- **Database Context**: Defines the structure of the database and relationships using EF Core's Code First approach.
- **Models**: Represent entities such as User, Chat, and Message.
- **Services**: Implement business logic to manage chats, messages, and users.

## Database Schema

The database schema includes the following entities:

- **Users**: Represented by the `User` model.
- **Chats**: Represented by the `Chat` model, including a relationship with the `User` who created the chat.
- **Messages**: Represented by the `Message` model, including relationships with both `User` and `Chat`.

## Unit and Integration Testing

- **Unit Tests**: Verify the functionality of individual components such as services and data access logic using xUnit and Moq for mocking dependencies.
- **Integration Tests**: Ensure that different parts of the application work together correctly, focusing on scenarios like creating chats, sending messages, and deleting chats.

## Demo Video
[screen-capture.webm](https://github.com/malikoyv/Chat-Application/assets/124885789/7ec0033e-9b15-4c86-bc7d-7608c9074145)

## Usage

- **API Endpoints**: Accessed through tools like Postman or directly through HTTP requests, providing functionality to manage users, create chats, send messages, and delete chats.
- **Websockets (SignalR)**: Used for real-time communication between clients, allowing users to see messages from others in the chat instantly.

## Deployment

- The application can be deployed to any hosting environment that supports ASP.NET Core applications and SQL Server databases.
- Ensure proper configuration of connection strings and environment settings for production deployment.

## Testing

- Use unit tests and integration tests to validate the application's functionality before deployment.
- Mock external dependencies in unit tests and ensure all edge cases are covered to maintain reliability.

## Notes

- **Authentication and Authorization**: Not implemented for simplicity. Each request must include a `UserId` to identify the user performing the action.
- **Error Handling**: Ensure robust error handling and validation for all API endpoints to provide meaningful feedback to clients.
- **Documentation**: Include Swagger/OpenAPI for API documentation if required.
- **Maintainability**: Follow SOLID principles and best practices to ensure the application is extendable and maintainable.

## Contributing

- Fork the repository, create a feature branch, make your changes, and submit a pull request.
- Ensure your code follows the existing coding style and conventions.

## License

- This project is licensed under the [MIT License](https://github.com/malikoyv/Chat-Application/blob/main/LICENSE).
