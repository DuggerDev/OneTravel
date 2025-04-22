# Configurations

## Server Configuration

### Default Server Configuration

The default server configuration is located in the `one_travel_server.conf` file. This file contains the default settings for the server, including the database connection and other settings.

```ini
[Server]
# Server Network Configuration
port = 8080
host = localhost

changes = false

[Database]
# Database Configuration
database = one_travel
database_type = sqlite
# Database Connection
database_connection = sqlite:///one_travel.db
# database_connection = postgresql://user:password@localhost:5432/one_travel
# database_connection = mysql://user:password@localhost:3306/one_travel

[Services]
# Services Configuration
# serivce_name = true
user_management = true
configuration = true
service_discovery = true
api_gateway = true
database = true
google_flights = true
fake_payment = true


```

### Server Configuration Section

| Option                  | Description                                                                   |
| ----------------------- | ----------------------------------------------------------------------------- |
| ----------------------- | ----------------------------------------------------------------------------- |
| `port`                  | The port on which the server will listen for incoming requests.               |
| `host`                  | The host on which the server will listen for incoming requests.               |
| `changes`               | If set to true, the server will check for changes in the configuration file and update the configuration table in the database. This is useful for development or deployment purposes. |

### Database Configuration Section

| Option                | Description                                                                                                           |
| --------------------- | --------------------------------------------------------------------------------------------------------------------- |
| `database`            | The name of the database to be used by the server.                                                                    |
| `database_type`       | The type of database to be used (e.g. sqlite, postgresql, mysql).                                                     |
| `database_connection` | The connection string for the database. This is optional and can be used to override the default database connection. |

### Services Configuration Section

| Option              | Description                                                                                                                                                                                                                                                       |
| ------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `service_name`      | name of the service found in the services directory. The service name is the name of the folder in the services directory. It only accepts boolean values to enable or disable the service. Configuration of each service is accessed in the configuration table. |
| `user_management`   | Enables the user management service. ( Mandatory )                                                                                                                                                                                                                  |
| `configuration`     | Enables the configuration service. ( Mandatory )                                                                                                                                                                                                                                |
| `service_discovery` | Enables the service discovery service. ( Mandatory )                                                                                                                                                                                                                           |
| `api_gateway`       | Enables the API gateway service. ( Mandatory )                                                                                                                                                                                                                                 |
| `database`          | Enables the database service. ( Mandatory )                                                                                                                                                                                                                                    |
| `google_flights`    | Enables the Google Flights service.                                                                                                                                                                                                                               |
| `fake_payment`      | Enables the Fake Payment service.                                                                                                                                                                                                                               |

### Service Configuration

Service configuration is stored in the database. Each service has its own configuration file located in the `services` directory. The configuration file is used to configure the service initially to create the database tables and include missing configurations in the configuration table. From then on, the service will use the configuration table to update the configuration file if changes are made to the configuration file, the 

### Service Configuration Options

The non-mandatory service configuration options are id'd by the service name and the option name. For example, the Google Flights service configuration option `google_flights_enabled` is the option to enable or disable the service.

The initial configuration file will look something like this:

``` ini
[Google Flights]
# Google Flights Service Configuration
google_flights_enabled = true
google_flights_key = YOUR_API_KEY
google_flights__url = https://api.google.com/flights
google_flights_timeout = 30
google_flights_retries = 3
google_flights_retry_delay = 5
google_flights_max_results = 100
google_flights_max_results_per_page = 10
google_flights_max_pages = 10
changes = true

```

This configuration file is then used to update the configuration table in the database. When the configuration table is updated, the configuration file is also updated with the new values. The configuration file is used to create the database tables and include missing configurations in the configuration table. From then on, the service will use the configuration table to update the configuration file if changes are made to the configuration file and the changes option is set to true, the service will update the configuration table with the new values.

What it looks like in the database:

| Option | Value |
| ------ | ----- |
| `google_flights_enabled` | true |
| `google_flights_key` | YOUR_API_KEY |
| `google_flights_url` | https://api.google.com/flights |
| `google_flights_timeout` | 30 |
| `google_flights_retries` | 3 |
| `google_flights_retry_delay` | 5 |
| `google_flights_max_results` | 100 |
| `google_flights_max_results_per_page` | 10 |
| `google_flights_max_pages` | 10 |


