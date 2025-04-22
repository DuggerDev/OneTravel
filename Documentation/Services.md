# Services

## Overview

### Core Services

The core services are the default services that are included in the application. These services are designed to provide the basic functionality of the application and are required for the application to function properly.

- **User Management**: A service that manages the user accounts and authentication in the application.
- **Configuration**: A service that manages the configuration of the application, including the database connection and other settings.
- **Service Discovery**: A service that manages the services in the application.
- **API Gateway**: A service that manages the API requests and responses in the application.
- **Database**: A service that manages the database connection and queries in the application.

#### Travel Services

- **Google Flights**: A service that manages the flight search and booking process, including searching for flights and making reservations.
- **Kayak**: A service that manages the hotel search and booking process, including searching for hotels and making reservations.

#### Functional Services

- **Rules Engine**: A service that manages the rules and policies for travel bookings, including travel policies, budgets, and reporting.
- **Booking**: A service that manages the booking process, including searching for flights and hotels, and making reservations.
- **Payment**: A service that manages the payment process, including processing payments and managing refunds.
- **Reporting**: A service that manages the reporting process, including generating reports on travel expenses and bookings.
- **Notification Engine**: A service that manages the notification process, including sending notifications to users about their bookings and travel policies.
- **Integrations**: A service that manages the integration with third-party systems, including SAP and PeopleSoft.

## Service Descriptions

### Core Services Descriptions

#### User Management

The user management service is responsible for managing the user accounts and authentication in the application.
This service will provide the a database based authentication system, and will be responsible for managing the user accounts and authentication in the application.

The administration users/groups are defined in the init_admins.conf file initially, when the application is started the service will first check if there is a connection to a database, if not it will use the init_admins.conf file to authenticate the user.

#### Configuration

The configuration service is responsible for managing the configuration of the application, including the database connection and other settings.

This service will update the configuration file when the application is started, and will be responsible for managing the configuration of the application.
If the configuration file [one_travel_server.conf](Configurations.md/#server-configuration) is not found, the service will create a new configuration file with the default settings.

#### Service Discovery

The Service Discovery service is responsible for managing the services in the application.
The `[Services]` section of the [one_travel_server.conf](Configurations.md/#server-configuration) file is used to look for enabled services and load the configuration files for each service for active use in the application.

When the application is started, the service discovery service will check the configuration file of each service to correctly create missing database tables, and update the services table for their use.

#### API Gateway

The API Gateway service is responsible for managing the API requests and responses in the application.
Using the configuration table and the services table, the api gateway will route the requests to the correct service, and return the response to the user.

#### Database

The database service is responsible for managing the database connection and queries in the application.
