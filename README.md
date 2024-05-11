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

### DataProcessor

**Description:**  
Aplikace konzumuje data z DataCollectoru, krtere zpracovava a uklada do databaze. Aplikace data formalizuje, tak aby ostatni systemy pracovali s jednotnym modelem. Data jsou formovany potrebam ostatnich systemu tak, abych se vyhnuly slozitejsim dotazum do databaze.

**Scaling:**  
Aplikaci podporuje horizontalni skalovani, pri nasazeni Azure container apps lze skalopat pomoci mnozstni zprav v ServiceBus fronte.

### Watchdog

**Description:**  
Aplikace konzumuje data z DataProcessoru a validuje je pomoci definovanych kriterii.

**Scaling:**  
Aplikaci podporuje horizontalni skalovani, pri nasazeni Azure container apps lze skalopat pomoci mnozstni zprav v ServiceBus fronte.

## Testing
Kod je strukturovany tak, aby podporoval testovani, kod domenove logiky neobsahuje zavyslost na externi systemy. Napojeni na externi systemy a infrastrukturu je vzdy pres interface, obsahujici nase obsatrakce, takze vse lze namockovat. Unit testy se zameri napr. vypocty hodnot a transformaci dat. Itegracni testy budou overovat spravnou inicializace WS klienta, jeho obnoveni v pripade vypadku a zpravne odpojeni.

## Scaling
Sluzby jsou bezstavove a podporuji horizontalni skalovani.

## Deployment
Aplikacni sluzby budou kontejenerizovany v ramci CI/CD pipeline a image budou ulozeni v repozitari, napr. Azure Container Repository. Odtud je lze nasadit a prenasadit do Azure Container App. Aplikaci lze provozovat i na VM nebo napr. Azure App Service. Azure Container App volim z duvodu jednoduche konfigurace a provozu, ktera ale prichazi s vyssimy naklady.
Jako databazi jsem zvolil Postgre, jako nejvyspelejsi open source databazi. V Azuru lze pouzit sluzbu Azure Database for PostgreSQL.

## Azure Running Costs

| Service        | Cost (per month) |
|----------------|------------------|
| DataCollector Container App | $200 |
| DataProcessor Container App | $200 |
| Watchdog Container App | $200 |
| Service bus          | $0.05 |
| Azure database for PostgreSQL      | $127|
| Total          | $727.05  |

Ceny lze snizit alokaci na 1 az 3 roky. Design aplikace neni primo vazan na tyto sluzby. Ceny dle drasticky snizit primim pouzi virtualniho storoje to vsak pouzaduje slozitejsi konfigurace a spravu.

## PoC implementation
Jedna se o hrubou ukazku toho, jak bych jednotlive sluzby implementoval. V ramci zjednoduseni zde nepouzivam veskete good practice, ktere bych pri implementaci produkcni aplikace pouzil, napr. misto primeho pristupu do db by existovala separatni sluzba, ktere by toto zajistovala a napr. by vyuzila cacheovani.

