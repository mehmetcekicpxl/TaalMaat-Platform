# TaalMaat - Digitaal Taalbuddy Platform

TaalMaat is een schaalbaar, veilig en maatschappelijk gedreven (Civic Tech) webplatform, ontwikkeld om anderstaligen (die Nederlands leren) en vrijwilligers digitaal met elkaar te verbinden. 

Dit project is oorspronkelijk ontwikkeld als graduaatsproef door **Mehmet CEKIC** (Graduaat Programmeren, PXL) binnen **PXL Smart ICT**, in samenwerking met **CodeForBelgium** en geïnspireerd door het praktijkinitiatief **VriendEnTaal** (Genk). 

Het primaire doel van dit platform is om de maatschappelijke integratie te versnellen door taalpraktijk voor iedereen toegankelijk te maken. Fysieke taalsessies zijn vaak moeilijk bij te wonen door alledaagse obstakels zoals onregelmatige werkuren, gebrek aan vervoer, lange afstanden of kinderopvang. 

Dit project biedt een flexibel, online alternatief waarmee anderstaligen eenvoudig een buddy kunnen vinden om digitaal te oefenen, zodat drempels zoals tijd en locatie volledig worden weggenomen.

---

##  Over het Project & Maatschappelijke Impact

Fysieke taaloefeningen zijn cruciaal voor integratie, maar niet iedereen kan zich verplaatsen naar vaste locaties op vaste tijdstippen. TaalMaat lost dit op door een toegankelijk, altijd beschikbaar digitaal platform te bieden.

**Sustainable Development Goals (SDG's) Impact:**
- **SDG 4 (Kwaliteitsonderwijs):** Toegankelijke leermogelijkheden via videocalls en zelfstudie-modules (Niveau A1 tot C2).
- **SDG 10 (Ongelijkheid verminderen):** Verlaagt de drempel voor mensen die door werk of gezinssituatie geen fysieke lessen kunnen bijwonen.
- **SDG 17 (Partnerschappen):** Ontwikkeld vanuit samenwerkingen met lokale overheden en vrijwilligersorganisaties.

---

##  Architectuur & Ontwerppatronen (Clean Architecture)

Het project volgt strikt de **Clean Architecture** principes. Dit zorgt voor een ontkoppelde, testbare en schaalbare codebase. Het systeem is bovendien **Multi-tenant** ontworpen (via de `Gemeente` entiteit), waardoor meerdere steden het platform tegelijk onafhankelijk kunnen gebruiken.

Als je als developer aan dit project werkt, is het cruciaal om te weten waar je code moet plaatsen:

### 1. TaalMaat.Core (Domein Laag)
De absolute kern van de applicatie. Bevat géén afhankelijkheden van Entity Framework of web frameworks.
- **`Entities/`:** Hier plaats je nieuwe database tabellen (bijv. `ApplicationUser`, `Sessie`, `Oefening`).
- **`Enums/`:** Definieer hier statische keuzelijsten (bijv. `GebruikerRol`, `BuddyStatus`).
- **`Interfaces/`:** Voeg hier de contracten (Interfaces) toe voor Repositories (`IRepository`) en externe Services.

### 2. TaalMaat.Application (Business Logica)
Bevat de regels van de applicatie. Afhankelijk van `.Core`, maar niet van `.Infrastructure` of de database.
- **`Services/`:** Maak hier nieuwe services aan als je logica moet toevoegen. (Bijv. `BuddyService` om matchingslogica te schrijven, of `OefeningService`). Injecteer altijd de Interfaces uit de Core-laag in de constructors van deze services.

### 3. TaalMaat.Infrastructure (Data & Externe Koppelingen)
Hier bevindt zich de concrete implementatie voor data-toegang en externe API's.
- **`Data/ApplicationDbContext.cs`:** Voeg hier je nieuwe `DbSet<T>` toe als je een nieuwe Entity hebt gemaakt en beheer hier je ModelBuilder configuraties (Fluent API).
- **`Repositories/`:** Implementeer hier de repository interfaces uit de Core laag (CRUD operaties via Entity Framework Core).
- **`Hubs/`:** SignalR hubs voor real-time communicatie (bijv. `NotificatieHub`).
- **`Services/`:** Specifieke technische implementaties zoals `EncryptieService` of externe API calls.

### 4. TaalMaat.WebUI (Presentatie Laag)
De Blazor Server frontend.
- **`Components/Pages/`:** Hier bouw je de schermen. Gedeeld per rol (Admin, Anderstalig, Vrijwilliger).
- **`Components/Shared/`:** Herbruikbare UI-componenten zoals dialogs en kaarten.
- **`Program.cs`:** Hier registreer je jouw nieuwe Repositories en Services in de Dependency Injection (DI) container. **Vergeet dit niet als je een nieuwe service aanmaakt!**

---

## Security & Technologische Keuzes

Tijdens de ontwikkeling zijn bewuste technologische keuzes gemaakt:
- **Blazor Server (.NET 8):** Gekozen voor snelle ontwikkeling met C# over de hele stack en naadloze real-time integratie.
- **Real-time Communicatie:** **SignalR** is gebruikt voor chat en notificaties vanwege de robuustheid en automatische reconnectie mechanismen.
-- **Videobellen (Jitsi):** In plaats van complexe WebRTC-verbindingen volledig zelf te ontwikkelen, integreert het platform met de open-source videobeltechnologie van Jitsi. Omdat recente publieke Jitsi-instanties vaak een moderatorlogin vereisen, werd gekozen voor de publieke open-source Jitsi-server van Freifunk München (`meet.ffmuc.net`), waar videogesprekken zonder verplichte accountregistratie mogelijk blijven. Deze keuze verhoogt de toegankelijkheid voor minder digitaal vaardige gebruikers aanzienlijk. Om ongewenste toegang te beperken, genereert het platform unieke en moeilijk voorspelbare (Universally Unique Identifier)UUID-gebaseerde sessielinks die enkel toegankelijk zijn via het afgeschermde portaal voor gekoppelde gebruikers.- **Encryptie & OWASP:** Chatberichten worden via AES-encryptie versleuteld opgeslagen in de databank. Voor elke encryptie wordt een unieke Initialization Vector (IV) gegenereerd, waardoor identieke berichten telkens verschillende cipherteksten produceren. Het systeem gebruikt daarnaast ASP.NET Core Identity voor password hashing en rol-gebaseerde autorisatie. Tijdens wachtwoordherstel werd een extra validatiestap via een “Geheim Woord” toegevoegd. Dit helpt om het herstelproces toegankelijker te maken voor minder digitaal vaardige gebruikers, terwijl extra verificatie behouden blijft.
*(Aanbeveling voor toekomstige developers: Implementatie van Rate-Limiting en 2FA staan nog open ter verbetering).*

---

##  Developer Guide: Hoe voeg ik een nieuwe feature toe?

Stel, je wilt een "Review/Beoordeling" systeem toevoegen na een videogesprek:
1. **Core:** Maak een `Review.cs` entity in `TaalMaat.Core/Entities/`. Maak een `IReviewRepository` interface in `TaalMaat.Core/Interfaces/`.
2. **Infrastructure:** Voeg `DbSet<Review>` toe in `ApplicationDbContext.cs`. Maak `ReviewRepository.cs` en implementeer `IReviewRepository`.
3. **Database updaten:** Draai `dotnet ef migrations add AddReviews` en daarna `dotnet ef database update`.
4. **Application:** Maak een `ReviewService.cs` in de Application laag met de logica ("een review mag maar 1 keer gegeven worden").
5. **WebUI (DI):** Ga naar `Program.cs` en voeg toe: `builder.Services.AddScoped<IReviewRepository, ReviewRepository>();` en `builder.Services.AddScoped<ReviewService>();`.
6. **WebUI (UI):** Maak de Blazor componenten (bijv. een `ReviewDialog.razor`) en roep de `ReviewService` aan.

---

##  Installatie en Lokale Setup

### 1. Vereisten
- .NET 8 SDK
- SQL Server (LocalDB of volledige versie)
- Visual Studio 2022 of vergelijkbare IDE

### 2. Stappen
1. **Clone de repository:**
   ```bash
   git clone https://github.com/mehmetcekicpxl/TaalMaat-Platform
   ```
2. **Database Connectie:** 
   Open het bestand `appsettings.json` (en `appsettings.Development.json`) in het project `TaalMaat.WebUI` en update de `DefaultConnection` met jouw database connectiestring.
3. **Migraties uitvoeren:**
   Zorg dat de database wordt opgebouwd:
   ```bash
   # Vanuit de root folder in je terminal:
   dotnet ef database update --project TaalMaat.Infrastructure\TaalMaat.Infrastructure.csproj --startup-project TaalMaat.WebUI\TaalMaat.WebUI.csproj
   ```
4. **Start de applicatie:**
   - Zet in Visual Studio `TaalMaat.WebUI` als Startup Project en druk op `F5`.

###  Seeding & Standaard Inloggegevens
Bij het eerste keer opstarten en uitvoeren van de migraties, populeert `Program.cs` automatisch de database met de basisgemeente (Genk), rollen,voor elk niveau 3 oefeningen (YouTube-links en tekst) 


---

##  Screenshots & UI


Gedetailleerde schermafbeeldingen die de basiswerking van het platform laten zien, zijn toegevoegd aan de map `docs/screenshots/` in het projectbestand. 

Afbeeldingen die u in deze map kunt vinden:
* Anderstalig (Studenten) paneel en overzicht (Dashboard)
* Vrijwilliger paneel en overzicht (Dashboard)
* Integratie van Jitsi-videogesprekken en een versleuteld chatvenster
* Stappen voor het resetten van het wachtwoord

Raadpleeg de betreffende map voor meer informatie over de gebruikersinterface.

---

##  Overdracht & Contact

Dit project is opgeleverd als werkend Minimum Viable Product (MVP). Voor verdere vragen over de codebase, architectuur of overdracht kan je contact opnemen met de oorspronkelijke ontwikkelaar of de projectbegeleiders vanuit PXL / CodeForBelgium.
