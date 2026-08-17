# TaalMaat Gebruikersrollen en Rechten

Dit document biedt een algemeen overzicht van de gebruikersrollen (Roles) binnen het TaalMaat-platform, bedoeld voor nieuwe ontwikkelaars en beheerders.

Er zijn drie hoofdrollen in het systeem:

## 1. Anderstalig (Taalleerder / Cursist)
Dit is de primaire doelgroep van het platform. Het zijn gebruikers die Nederlands  willen leren en oefenen. Ze hebben directe toegang tot de applicatie.

**Belangrijkste functies en rechten:**
* **Zelfstandig zoeken (Zelf Matching):** Er is **geen automatisch matchingsysteem** vanuit het platform of de admins. De anderstalige zoekt zelf een geschikte Vrijwilliger uit de lijst en stuurt een connectieverzoek.
* **Meerdere Buddies:** Een anderstalige mag meerdere vrijwilligers (buddies) tegelijk hebben.
* **Zelfstudie (Oefeningen):** Ze kunnen video-, audio- en tekstgebaseerde oefeningen maken en direct zien of een antwoord goed of fout is. 
* **Communicatie:** Ze kunnen via chat berichten sturen naar de vrijwilligers met wie ze gekoppeld zijn.
* **Rapporteren:** Ze kunnen ongepaste situaties of gebruikers direct rapporteren aan de Admin.

## 2. Vrijwilliger (TaalMaatje)
Dit zijn personen die zeer goed Vlaams spreken en Anderstalige gebruikers vrijwillig willen helpen bij het leren van de taal.
**Belangrijkste functies en rechten:**
* **Verzoeken beheren (Accepteren/Weigeren):** Ze ontvangen verzoeken van anderstaligen en bepalen helemaal zelf of ze een verzoek accepteren of weigeren. 
* **Bescherming werkdruk:** Om overbelasting te voorkomen, verdwijnen vrijwilligers automatisch uit de zoeklijsten van anderstaligen zodra ze 3 actieve buddies hebben.
* **Beschikbaarheid delen:** Ze kunnen hun beschikbare uren of dagen delen op hun profiel.
* **Communicatie:** Ze chatten met hun cursisten om zo dagelijkse conversatie te oefenen.
* **Rapporteren:** Ze kunnen ongepaste situaties of gebruikers direct rapporteren aan de Admin.

## 3. Admin (Systeembeheerder)
Dit zijn de beheerders die verantwoordelijk zijn voor de algehele werking, het contentbeheer en de veiligheid van het platform. Zij bemoeien zich niet met de actieve matching tussen gebruikers.

**Belangrijkste functies en rechten:**
* **Vrijwilligers keuren:** Ze moeten aanmeldingen van nieuwe vrijwilligers goedkeuren of afwijzen.
* **Gemeenten beheren:** Ze voegen nieuwe gemeenten  toe aan het systeem wanneer deze zich registreren.
* **Contentbeheer:** Ze maken nieuwe oefeningen en content aan in de database.
* **Moderatie en Sancties:** Ze kunnen gebruikers tijdelijk of permanent uit het systeem verwijderen of blokkeren.
* **Geschillen oplossen (Inzage chats):** Wanneer een conflict gemeld wordt via een rapportage, mag de Admin de chatgeschiedenis tussen de twee betrokken personen inzien om de situatie te beoordelen.

> **Toekomstvisie (Roadmap):** In de toekomst zal de Admin rol opgesplitst worden in een **Superadmin** (bijv. Code for Belgium, die het hele systeem beheert) en een **Lokale Admin** (bijv. iemand van een gemeente of VriendenTaal, die alleen zijn eigen lokale gebruikers beheert). Momenteel is deze scheiding nog niet gemaakt.

---

### Opmerkingen voor Ontwikkelaars (Identity Structuur)
Het systeem maakt gebruik van de **ASP.NET Core Identity** infrastructuur. In de code worden rollen gecontroleerd met attributen zoals `[Authorize(Roles = "Admin")]`. 

Wanneer je een nieuw scherm ontwikkelt, houd er dan strikt rekening mee dat de matching volledig **gebruiker-gestuurd** is (geen systeemautomatisering) en dat de voortgang van anderstaligen momenteel nergens permanent wordt opgeslagen.
