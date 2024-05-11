# Watchdog

## General Information

Navrh a POC implemetnace systemu pro monitoring obchodnich aktivit. Aplikace je rozdelana do tri samostanychc sluzeb bezic v Azure container app. Sluzby mezi sebou komunikuji pomoci Service busu. Data jsou ulozena v Posrgre databazi.

![Screenshot 2024-05-11 at 10 38 29](https://github.com/JiriNavratildev/watchdog/assets/121182964/eb071261-e8a9-4000-a824-6cf638fc782b)

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

| Service        | Cost (per month) |
|----------------|------------------|
| DataCollector Container App | $200 |
| DataProcessor Container App | $200 |
| Watchdog Container App | $200 |
| Service bus          | $0.05 |
| Azure database for PostgreSQL      | $127|
| Total          | $727  |


