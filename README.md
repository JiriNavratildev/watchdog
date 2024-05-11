# Watchdog
![Screenshot 2024-05-11 at 9 23 21](https://github.com/JiriNavratildev/watchdog/assets/121182964/b7a56950-08fc-4566-9258-c72c4508bf4d)

## General Information

[Add general information about your software architecture here.]

## Services

### DataCollector

**Description:**  
Slouzi pro sber dat z definovanych zdroju. Pro kazdy zdroj implementuje klieta, ktery aktivne sbira data a service bus fronty pro dalsi spracovani.

**Scaling:**  
Jednotlive datove zdroje mohou mit vlastni instaci pro lepsi skalovani.

**Testing:**  
[Details about testing procedures for the DataCollector service.]

**Deployment:**  
Konteinerizovana instace pobezi v Azure container apps.

### DataProcessor

**Description:**  
Aplikace konzumuje data z DataCollectoru, krtere formalizuje a aklada do databaze.

**Scaling:**  
Aplikaci podporuje horizontalni skalovani, pri nasazeni Azure container apps lze skalopat pomoci mnozstni zprav v ServiceBus fronte.

**Testing:**  

**Deployment:**  
Konteinerizovana instace pobezi v Azure container apps.

### Watchdog

**Description:**  
Aplikace konzumuje data z DataProcessoru a validuje je pomoci definovanych kriterii.

**Scaling:**  
Aplikaci podporuje horizontalni skalovani, pri nasazeni Azure container apps lze skalopat pomoci mnozstni zprav v ServiceBus fronte.

**Testing:**  
[Details about testing procedures for the Watchdog service.]

**Deployment:**  
Konteinerizovana instace pobezi v Azure container apps.

## Azure Running Costs

| Service        | Cost (per month) | Notes                                      |
|----------------|------------------|--------------------------------------------|
| DataCollector  | $XX              | [Notes about DataCollector costs.]         |
| DataProcessor  | $XX              | [Notes about DataProcessor costs.]         |
| Watchdog       | $XX              | [Notes about Watchdog costs.]              |
| Total          | $XX              | [Any additional notes about total costs.]  |
| Azure container Registry          | $XX              | [Any additional notes about total costs.]  |
| Service bus          | $XX              | [Any additional notes about total costs.]  |

