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
Tato komponenta obsahuje minimum domenove logiky, proto zde budou primarne integracni testy, aplikace pouziva abstarakce, takze externi sluzby lze namockovat. Testova se bude primarne web socket clint, propojeni, zpracovani zpravy, osetreni vypadku spojeni a spravne odpojeni.

**Deployment:**  
Konteinerizovana instace pobezi v Azure container apps.

### DataProcessor

**Description:**  
Aplikace konzumuje data z DataCollectoru, krtere zpracovava a uklada do databaze. Aplikace data formalizuje, tak aby ostatni systemy pracovali s jednotnym modelem. Data jsou formovany potrebam ostatnich systemu tak, abych se vyhnuly slozitejsim dotazum do databaze.

**Scaling:**  
Aplikaci podporuje horizontalni skalovani, pri nasazeni Azure container apps lze skalopat pomoci mnozstni zprav v ServiceBus fronte.

**Testing:**  
Parsovani a formalizace zprav budou pokryty unit testy. Konzumaci zprav lze testovat pomoci mock instaci.

**Deployment:**  
Konteinerizovana instace pobezi v Azure container apps.

### Watchdog

**Description:**  
Aplikace konzumuje data z DataProcessoru a validuje je pomoci definovanych kriterii.

**Scaling:**  
Aplikaci podporuje horizontalni skalovani, pri nasazeni Azure container apps lze skalopat pomoci mnozstni zprav v ServiceBus fronte.

**Testing:**  
Domenova logika lze testovat unit testy. 
// Sjednotime to testovani do vlastni kategrie, nema smysl to psat zvlast, aji ten scaling i guess

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
| Total          | $727.05  |

Ceny lze snizit alokaci na 1 az 3 roky. Design aplikace neni primo vazan na tyto sluzby. Ceny dle drasticky snizit primim pouzi virtualniho storoje to vsak pouzaduje slozitejsi konfigurace a spravu.

