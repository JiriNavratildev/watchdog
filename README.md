# Watchdog

## General Information

Navrh a POC implemetnace systemu pro monitoring obchodnich aktivit. Aplikace je rozdelana do tri samostanychc sluzeb, bezi v Azuru.

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
Aplikace konzumuje data z DataProcessoru a validuje je pomoci definovanych kriterii.
Kazdy novy deal je validovan oproti existujicim v databazi.

## Testing
Kod je strukturovany tak, aby podporoval testovani, kod domenove logiky neobsahuje zavyslost na externi systemy. Napojeni na externi systemy a infrastrukturu je vzdy pres interface, obsahujici nase obsatrakce, takze vse lze namockovat. Unit testy se zameri napr. vypocty hodnot a transformaci dat. Itegracni testy budou overovat spravnou inicializace WS klienta, jeho obnoveni v pripade vypadku a zpravne odpojeni.

## Scaling
Sluzby jsou bezstavove a podporuji horizontalni skalovani. Pri nasazeni Azure container apps lze skalopat pomoci mnozstni zprav v Azure Service Bus fronte.

## Deployment
Aplikacni sluzby budou kontejenerizovany v ramci CI/CD pipeline a image budou ulozeni v repozitari, napr. Azure Container Repository. Odtud je lze nasadit a prenasadit do Azure Container App. Aplikaci lze provozovat i na VM nebo napr. Azure App Service. Azure Container App volim z duvodu jednoduche konfigurace a provozu, ktera ale prichazi s vyssimy naklady.
Jako databazi jsem zvolil Postgre, jako nejvyspelejsi open source databazi. V Azuru lze pouzit sluzbu Azure Database for PostgreSQL.
Pro asynchroni komunikaci se pouziva message broker Azure Service Bus. 

## Model
![Screenshot 2024-05-11 at 17 11 04](https://github.com/JiriNavratildev/watchdog/assets/121182964/0d0600f3-ced3-4b88-8f8f-93e390ff0e7c)

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

Ceny lze snizit alokaci na 1 az 3 roky. Design aplikace neni primo vazan na tyto sluzby. Ceny dle drasticky snizit primim pouzi virtualniho storoje to vsak pouzaduje slozitejsi konfigurace a spravu.

## PoC implementation
Jedna se o hrubou ukazku toho, jak bych jednotlive sluzby implementoval. V ramci zjednoduseni zde nepouzivam veskete good practice, ktere bych pri implementaci produkcni aplikace pouzil, napr. misto primeho pristupu do db by existovala separatni sluzba, ktere by toto zajistovala a napr. by vyuzila cacheovani.

## Monitoring
Pomoci Application Insights budeme monitorovat metriky CPU a GPU vsech instaci a databaze. Dale applikacni logy a errory.

## Co je potreba specifikovat?
- Chceme monitorovat i limitni ordery nebo jen market?
- Jaka je casova tolerance odhaleni problemu?
- Chceme ukladat vsechna data? Nebo jen duplicitni ordery?
- Je potreba aktualizovat konfiguraci za behu?
- Jakym zpusobem bude probihat notifikace?

