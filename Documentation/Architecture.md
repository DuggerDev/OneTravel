# Architecture

## Overview

### The Personal Traveler

In the first iteration of this project our sole focus is to build a simple travel application that allows users to search for flights and hotels, and book them.
The application is designed to be user-friendly and intuitive, allowing users to easily navigate through the various features and functionalities.
The primary goal is to provide a seamless experience for users when searching for and booking travel accommodations.

This initial approach is focused on the personal traveler and lays the default implementation for an extended and more complex [Enterprise Travel Management](#enterprise-travel-management) system. Default included services are noted in the [Services Page](Services.md).

### Enterprise Travel Management

When enterprises approach how to pay for travel for their employees, many companies have the following issues:

- **Travel Policies**: Companies have different travel policies for different departments, and they need to ensure that employees are following the correct policy.
In addition, companies have different travel policies for different types of travel (e.g. domestic vs international), department, and specific employees.
- **Travel Providers**: Companies may have different travel providers and/or booking agencies for different departments.
For example an university may have different travel providers for athletics versus the rest of the faculty, and staff.
- **Travel Budgets**: Companies may have different travel budgets for different departments, and they need to ensure that employees are staying within the budget.
- **Travel Reporting**: Companies need to be able to report on travel expenses by department, and they need to be able to report on travel expenses by employee.
- **Travel Reimbursement**: Companies need to be able to reimburse employees for travel expenses, and they need to be able to report on travel expenses by employee.

The list of issues is not exhaustive, but it does provide a good overview of the types of issues that companies face when managing travel for their employees.
The goal of this project is to provide a flexible and easily configurable solution that can be tailored to the specific needs of each company, by introducing pluggable services.

## Model-View-Controller (MVC) Architecture (Frontend)

The frontend is constructed using a Model-View-Controller (MVC) architecture for its well understood and structured approach to organizing code.
The architecture pattern is designed to separate the application into three interconnected components:

- **Model**: Represents the data and business logic of the application. It is responsible for managing the data, including retrieving, storing, and processing it.
- **View**: Represents the user interface of the application. It is responsible for displaying data to the user and capturing user input.
- **Controller**: Acts as an intermediary between the Model and View. It processes user input, interacts with the Model, and updates the View accordingly.

The MVC architecture promotes a clear separation of concerns, making it easier to manage and maintain the codebase. It allows developers to work on different components independently, facilitating collaboration and reducing the risk of introducing bugs.

In the context of this project, while the model is defined as representing the business logic, we opted to pass the business logic to the service-based architecture in the backend.
In effect this makes our MVC architecture the user interface layer of the application, which is responsible for displaying data to the user and capturing user input and passing it to the backend through an API facade.

## Service-Based Architecture (Backend)

The backend is constructed using a service-based architecture, which is designed to provide a quickly deployable solution for managing the various services that are used in the application.
To ensure flexibility is provided while maintaining the core functionality of the application, the database will leverage the use of partitioning to separate the data for each service.
This makes adding and removing services easy as simply dropping a folder in the service directory and enabling the service in the configuration file.

When processing a request, the backend will first check the configuration file to determine which services are enabled, use the applicable services in the correct stages of the request, and then return the results to the frontend.

This can include flight and hotel providers, booking agencies, and other travel-related services.

Other Example services could include:

- **A Rules Engine**: A service that manages the rules and policies for travel bookings, including travel policies, budgets, and reporting.
- **Booking (Google Flights/Kayak)**: A service that manages the booking process, including searching for flights and hotels, and making reservations.
- **Payment (Swipe/Amazon Pay/Apple Pay)**: A service that manages the payment process, including processing payments and managing refunds.
- **Reporting**: A service that manages the reporting process, including generating reports on travel expenses and bookings.
- **Notification Engine**: A service that manages the notification process, including sending notifications to users about their bookings and travel policies.
- **Integrations**: A service that manages the integration with third-party systems, including SAP and PeopleSoft.

For the purposes of this project, the default configuration will be to implement at least one Booking service (Google Flights) and one Payment service (a fake payment service).
