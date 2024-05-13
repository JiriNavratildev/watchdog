# Watchdog

## General Information

Design and POC implementation of a trading activity monitoring system.

![Screenshot 2024-05-11 at 10 38 29](https://github.com/JiriNavratildev/watchdog/assets/121182964/eb071261-e8a9-4000-a824-6cf638fc782b)

## Services

### DataCollector

**Description:**  
Gathers data from specified sources, implementing clients for each and establishing web socket connections for real-time updates. In the DxTrade add-on, it receives portfolio and user account updates, forwarding data to Azure Service Bus for processing. Its extensibility allows for easy integration of additional data sources, ensuring adaptability to evolving needs.

### DataProcessor

**Description:**  
Consumes data from the DataCollector, processes it, and stores it in the database. It standardizes the data to ensure compatibility with other systems, reducing the need for complex database queries. Upon application startup, background jobs initiate processing for individual queues. Processed data is then stored in the database and transmitted to the service bus for additional processing.

### Watchdog

**Description:**  
Consumes data from the DataProcessor and validates it against predefined criteria. It compares each new deal with existing ones in the database, triggering notifications upon finding a match.

## Testing
The code is designed for testing with a focus on isolating domain logic from external dependencies. All interactions with external systems and infrastructure occur through interfaces, allowing for easy mocking. Unit tests primarily cover value calculations and data transformations, while integration tests validate proper initialization, recovery, and disconnection of the WebSocket client.

## Scaling
Services are stateless and support horizontal scaling. Azure container apps can scale up or down based on message count in the Azure Service Bus queue.

## Deployment
Application services will be containerized in the CI/CD pipeline and deployed to Azure Container App or other options like VMs or Azure App Service. Postgre is selected as the database, leveraging Azure Database for PostgreSQL service. The Azure Service Bus is used for asynchronous communication.

## Model
![Screenshot 2024-05-12 at 19 29 59](https://github.com/JiriNavratildev/watchdog/assets/121182964/0e8de203-d0ab-44d5-a68c-ede8e09117e0)

## Azure Running Costs

| Service        | Cost (per month) |
|----------------|------------------|
| DataCollector Container App | $200 |
| DataProcessor Container App | $200 |
| Watchdog Container App | $200 |
| Service bus          | $0.05 |
| Azure database for PostgreSQL      | $127|
| Application Insights      | $0|
| Total          | $727.05  |

Prices can be reduced by opting for 1 to 3-year allocations. The application design is independent of these services. Considerable cost savings can be achieved by utilizing virtual machines, though this will require more complex setup and maintenance.

## PoC implementation
This is a basic demonstration of how I would implement individual services. For simplicity, I've omitted certain best practices typically employed in production applications. For instance, instead of direct database access, a separate service would handle this, possibly incorporating caching mechanisms.

## Monitoring
Application Insights will be used to monitor CPU and memory metrics across all installations and the database, as well as to track application logs and requests.

## What yet needs to be clarified?
- Do we need to monitor limit orders or just market?
- What is the time tolerance for toxic user detection?
- What will be the notification process?

