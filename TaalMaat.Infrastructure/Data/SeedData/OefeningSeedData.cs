using TaalMaat.Core.Entities;
using TaalMaat.Core.Enums;

namespace TaalMaat.Infrastructure.Data.SeedData;

/// <summary>
/// Seed data voor taaloefeningen: per niveau (A1 t/m C2) drie oefeningen
/// (video, tekst, audio) met elk twee meerkeuzevragen.
/// Alle YouTube-links verwijzen naar echte, publiek beschikbare video's
/// gericht op het leren van het Belgisch-Nederlands (Vlaams).
/// </summary>
public static class OefeningSeedData
{
    public static List<Oefening> GetOefeningen()
    {
        return new List<Oefening>
        {
            // ╔══════════════════════════════════════════════════════════════╗
            // ║                         A1 - BEGINNER                       ║
            // ╚══════════════════════════════════════════════════════════════╝

            // --- A1: VIDEO ---
            new Oefening
            {
                Titel = "Kennismaken – Jezelf voorstellen (A1)",
                Niveau = OefeningNiveau.A1,
                YouTubeUrl = "https://www.youtube.com/embed/26lJiDAQUYg",
                Inhoud = "Bekijk de video over jezelf voorstellen in het Nederlands. Let op de begroetingen en hoe je je naam, leeftijd en woonplaats vertelt.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Hoe zeg je 'My name is...' in het Nederlands?",
                        OptiesJson = "[\"Mijn naam is...\",\"Ik heet niet...\",\"Hij heet...\",\"Wij heten...\"]",
                        JuistAntwoord = "Mijn naam is..."
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Welke begroeting gebruik je 's ochtends?",
                        OptiesJson = "[\"Goedemorgen\",\"Goedenavond\",\"Goedenacht\",\"Tot ziens\"]",
                        JuistAntwoord = "Goedemorgen"
                    }
                }
            },

            // --- A1: TEKST ---
            new Oefening
            {
                Titel = "Boodschappen doen – Leestekst (A1)",
                Niveau = OefeningNiveau.A1,
                YouTubeUrl = null,
                AudioUrl = null,
                Inhoud = @"Anna gaat naar de supermarkt. Ze heeft een boodschappenlijst.

Op de lijst staat:
- 1 liter melk
- 6 eieren
- 1 brood
- 2 appels
- 1 pak kaas

Anna pakt een mandje. Ze loopt door de winkel. Eerst pakt ze de melk. De melk kost 1 euro en 20 cent. Dan pakt ze de eieren. De eieren kosten 2 euro en 50 cent.

Bij de kassa betaalt Anna met haar pinpas. De cassière zegt: 'Dat is 9 euro en 80 cent, alstublieft.'
Anna zegt: 'Dank u wel. Dag!'
De cassière zegt: 'Dag mevrouw, tot ziens!'",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Waar gaat Anna naartoe?",
                        OptiesJson = "[\"Naar de supermarkt\",\"Naar het ziekenhuis\",\"Naar school\",\"Naar het park\"]",
                        JuistAntwoord = "Naar de supermarkt"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Hoeveel kosten de eieren?",
                        OptiesJson = "[\"2 euro en 50 cent\",\"1 euro en 20 cent\",\"9 euro en 80 cent\",\"3 euro\"]",
                        JuistAntwoord = "2 euro en 50 cent"
                    }
                }
            },

            // --- A1: AUDIO ---
            new Oefening
            {
                Titel = "Nederlands leren voor beginners – Crash Course (A1)",
                Niveau = OefeningNiveau.A1,
                YouTubeUrl = "https://www.youtube.com/embed/604lSbUeSQ4",
                AudioUrl = null,
                Inhoud = "Bekijk deze crash course van 'Dutch with Eline'. Je leert hier de basis van het Nederlands, zoals begroeten, jezelf voorstellen en belangrijke werkwoorden. Let goed op de Vlaamse uitspraak.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Wat is een veelgebruikte informele begroeting in Vlaanderen?",
                        OptiesJson = "[\"Hoi\",\"Goedemorgen\",\"Tot ziens\",\"Dank u\"]",
                        JuistAntwoord = "Hoi"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Welk werkwoord wordt vaak gebruikt om jezelf voor te stellen?",
                        OptiesJson = "[\"Zijn\",\"Lopen\",\"Slapen\",\"Fietsen\"]",
                        JuistAntwoord = "Zijn"
                    }
                }
            },

            // ╔══════════════════════════════════════════════════════════════╗
            // ║                       A2 - ELEMENTAIR                       ║
            // ╚══════════════════════════════════════════════════════════════╝

            // --- A2: VIDEO ---
            new Oefening
            {
                Titel = "Werkwoorden: Kunnen, mogen, moeten (A2)",
                Niveau = OefeningNiveau.A2,
                YouTubeUrl = "https://www.youtube.com/embed/h9I_6q9_W3I",
                Inhoud = "In deze video leer je hoe je de modale werkwoorden 'kunnen', 'mogen' en 'moeten' gebruikt in een zin. Dit is erg belangrijk in het dagelijks leven.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Welk werkwoord gebruik je als je verplicht bent om iets te doen?",
                        OptiesJson = "[\"Moeten\",\"Mogen\",\"Kunnen\",\"Willen\"]",
                        JuistAntwoord = "Moeten"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Welk werkwoord gebruik je als je toestemming hebt om iets te doen?",
                        OptiesJson = "[\"Mogen\",\"Kunnen\",\"Moeten\",\"Zullen\"]",
                        JuistAntwoord = "Mogen"
                    }
                }
            },

            // --- A2: TEKST ---
            new Oefening
            {
                Titel = "Een dag in het leven van Mark – Leestekst (A2)",
                Niveau = OefeningNiveau.A2,
                YouTubeUrl = null,
                AudioUrl = null,
                Inhoud = @"Mark staat elke dag om 7 uur op. Hij doucht en kleedt zich aan. Om half acht eet hij ontbijt: twee boterhammen met kaas en een kop koffie.

Om 8 uur fietst Mark naar zijn werk. Hij werkt op een kantoor in het centrum van de stad. Zijn collega's zijn erg aardig. Om 12 uur heeft hij pauze. Hij eet een broodje en drinkt thee.

Na het werk gaat Mark naar de sportschool. Hij sport twee keer per week. Daarna kookt hij het avondeten. Vanavond maakt hij pasta met tomatensaus.

Om 10 uur gaat Mark naar bed. Hij leest nog even een boek en dan slaapt hij. Morgen moet hij weer vroeg op.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Hoe laat staat Mark op?",
                        OptiesJson = "[\"Om 7 uur\",\"Om 6 uur\",\"Om 8 uur\",\"Om half acht\"]",
                        JuistAntwoord = "Om 7 uur"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Hoe gaat Mark naar zijn werk?",
                        OptiesJson = "[\"Met de fiets\",\"Met de auto\",\"Met de bus\",\"Te voet\"]",
                        JuistAntwoord = "Met de fiets"
                    }
                }
            },

            // --- A2: AUDIO ---
            new Oefening
            {
                Titel = "Hulp vragen: Wil je me helpen? (A2)",
                Niveau = OefeningNiveau.A2,
                YouTubeUrl = "https://www.youtube.com/embed/8FDxjrJ2j2k",
                AudioUrl = null,
                Inhoud = "Luister naar deze dialoog over hulp vragen in het Nederlands. Leer hoe je beleefd vraagt of iemand je kan helpen met een probleem.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Hoe vraag je beleefd om hulp in het Nederlands?",
                        OptiesJson = "[\"Wil je me helpen?\",\"Ik wil hulp nu\",\"Help mij direct\",\"Wat doe je?\"]",
                        JuistAntwoord = "Wil je me helpen?"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Welk antwoord is positief als iemand om hulp vraagt?",
                        OptiesJson = "[\"Ja natuurlijk\",\"Nee ik heb geen tijd\",\"Misschien morgen\",\"Ik weet het niet\"]",
                        JuistAntwoord = "Ja natuurlijk"
                    }
                }
            },

            // ╔══════════════════════════════════════════════════════════════╗
            // ║                      B1 - INTERMEDIATE                      ║
            // ╚══════════════════════════════════════════════════════════════╝

            // --- B1: VIDEO ---
            new Oefening
            {
                Titel = "Typische fouten in het Vlaams (B1)",
                Niveau = OefeningNiveau.B1,
                YouTubeUrl = "https://www.youtube.com/embed/SpABYKct0yM",
                Inhoud = "In deze video bespreekt Alexia veelvoorkomende fouten die mensen maken als ze Vlaams leren. Ze legt uit hoe je natuurlijker kunt klinken en de juiste woorden kiest.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Wat is het doel van deze video?",
                        OptiesJson = "[\"Fouten vermijden om natuurlijker Vlaams te spreken\",\"Leren fietsen\",\"Geschiedenis van België leren\",\"Recepten voor wafels\"]",
                        JuistAntwoord = "Fouten vermijden om natuurlijker Vlaams te spreken"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Waarom is het belangrijk om typische fouten te herkennen?",
                        OptiesJson = "[\"Om beter begrepen te worden in Vlaanderen\",\"Omdat het leuk is\",\"Om boeken te schrijven\",\"Om sneller te rennen\"]",
                        JuistAntwoord = "Om beter begrepen te worden in Vlaanderen"
                    }
                }
            },

            // --- B1: TEKST ---
            new Oefening
            {
                Titel = "Het Nederlandse weer – Leestekst (B1)",
                Niveau = OefeningNiveau.B1,
                YouTubeUrl = null,
                AudioUrl = null,
                Inhoud = @"België staat bekend om zijn wisselvallige weer. Het kan op één dag zonnig, bewolkt en regenachtig zijn. Vlamingen hebben daar zelfs een gezegde voor: 'Je beleeft vier seizoenen op één dag.'

De winter in België is meestal mild, met temperaturen rond de 2 tot 7 graden Celsius. Af en toe vriest het en kan het sneeuwen, maar strenge winters zijn zeldzaam geworden door de klimaatverandering.

In de lente wordt het langzaam warmer. Dit is de tijd van de bloeiende natuur. Veel mensen bezoeken dan de parken en bossen, zoals het Hallerbos dat vol staat met boshyacinten.

De zomer kan warm zijn, met temperaturen die soms boven de 30 graden uitkomen. Veel Vlamingen gaan dan naar de Belgische kust of fietsen door de Kempen. Maar ook in de zomer kan het plotseling gaan regenen.

De herfst is vaak grijs en nat, maar ook prachtig door de kleurrijke bladeren aan de bomen. Het is de tijd om gezellig binnen te zitten met een warme wafel of een kom soep.

Belgen praten graag over het weer. Het is een populair onderwerp bij de koffieautomaat of bij de bakker. Bijna iedereen heeft een paraplu bij zich – 'voor het geval dat'.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Wat is typisch voor het Belgische weer?",
                        OptiesJson = "[\"Het is wisselvallig\",\"Het is altijd zonnig\",\"Het sneeuwt elke dag\",\"Het is altijd warm\"]",
                        JuistAntwoord = "Het is wisselvallig"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Wat doen veel Vlamingen in de lente?",
                        OptiesJson = "[\"Parken en bossen bezoeken\",\"Altijd binnen blijven\",\"Naar het buitenland verhuizen\",\"Schaatsen op natuurijs\"]",
                        JuistAntwoord = "Parken en bossen bezoeken"
                    }
                }
            },

            // --- B1: AUDIO ---
            new Oefening
            {
                Titel = "Belangrijke Vlaamse woorden (B1)",
                Niveau = OefeningNiveau.B1,
                YouTubeUrl = "https://www.youtube.com/embed/62ghn7JfmzA",
                AudioUrl = null,
                Inhoud = "Leer handige en veelgebruikte woorden in het Belgisch-Nederlands (Vlaams). In dit deel ligt de focus op het beschrijven van dingen (heel groot, heel klein).",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Welk onderwerp wordt in de video besproken?",
                        OptiesJson = "[\"Woordenschat om dingen te beschrijven\",\"Belgische politiek\",\"Hoe je frieten maakt\",\"De Nederlandse grammatica\"]",
                        JuistAntwoord = "Woordenschat om dingen te beschrijven"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Waarom focust de video specifiek op 'Belgisch-Nederlands'?",
                        OptiesJson = "[\"Omdat er soms andere woorden worden gebruikt dan in Nederland\",\"Omdat het makkelijker is\",\"Omdat het sneller is\",\"Omdat er geen grammatica is\"]",
                        JuistAntwoord = "Omdat er soms andere woorden worden gebruikt dan in Nederland"
                    }
                }
            },

            // ╔══════════════════════════════════════════════════════════════╗
            // ║                   B2 - UPPER INTERMEDIATE                   ║
            // ╚══════════════════════════════════════════════════════════════╝

            // --- B2: VIDEO ---
            new Oefening
            {
                Titel = "Welke zin is juist? – Grammaticatest (B2)",
                Niveau = OefeningNiveau.B2,
                YouTubeUrl = "https://www.youtube.com/embed/WRXAc3FxBjA",
                Inhoud = "Test je grammaticale kennis met deze video. Je krijgt verschillende zinnen te zien en moet bepalen welke zin correct is opgebouwd volgens de standaard norm.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Wat moet je doen tijdens deze video-oefening?",
                        OptiesJson = "[\"Kiezen welke zin correct is\",\"Een liedje zingen\",\"Een tekst voorlezen\",\"Woorden spellen\"]",
                        JuistAntwoord = "Kiezen welke zin correct is"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Wat is belangrijk bij het bepalen van de juiste zin?",
                        OptiesJson = "[\"De woordvolgorde en grammatica\",\"De lengte van de zin\",\"Hoeveel letters erin zitten\",\"De kleur van de tekst\"]",
                        JuistAntwoord = "De woordvolgorde en grammatica"
                    }
                }
            },

            // --- B2: TEKST ---
            new Oefening
            {
                Titel = "De Belgische arbeidsmarkt – Leestekst (B2)",
                Niveau = OefeningNiveau.B2,
                YouTubeUrl = null,
                AudioUrl = null,
                Inhoud = @"De Belgische arbeidsmarkt is de afgelopen jaren sterk veranderd. De opkomst van flexibele contracten, zelfstandigen en de platformeconomie heeft de traditionele werkrelatie flink veranderd.

Een belangrijk kenmerk in België is het sterke sociaal overleg, waarbij vakbonden, werkgevers en de overheid samen onderhandelen over cao's (collectieve arbeidsovereenkomsten). Dit model heeft bijgedragen aan de indexering van de lonen, wat betekent dat lonen automatisch stijgen als het leven duurder wordt.

Toch zijn er grote uitdagingen. Er is een zogenaamde 'krapte op de arbeidsmarkt' – een tekort aan arbeidskrachten in sectoren zoals de zorg, het onderwijs, IT en de bouw. Bedrijven zoeken steeds vaker naar specifiek geschoolde profielen en zogenaamde knelpuntberoepen raken moeilijk ingevuld.

Daarnaast speelt de vergrijzing een belangrijke rol. Omdat veel oudere werknemers met pensioen gaan, is de instroom van nieuw talent cruciaal. Dit biedt veel kansen voor anderstalige nieuwkomers op de arbeidsmarkt, op voorwaarde dat zij de juiste opleidingen volgen en de taal voldoende beheersen.

Voor anderstaligen is kennis van de Vlaamse werkcultuur essentieel. Punctualiteit, direct maar beleefd overleggen, en het tonen van eigen initiatief worden op de werkvloer zeer gewaardeerd.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Wat is een uniek kenmerk van het Belgische systeem voor werknemers?",
                        OptiesJson = "[\"De automatische indexering van de lonen\",\"Dat niemand belasting betaalt\",\"Dat je altijd thuis mag werken\",\"Dat er geen vakbonden zijn\"]",
                        JuistAntwoord = "De automatische indexering van de lonen"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Wat wordt bedoeld met een 'knelpuntberoep'?",
                        OptiesJson = "[\"Een beroep waarvoor erg moeilijk personeel te vinden is\",\"Een beroep zonder stress\",\"Een baan in de politiek\",\"Een beroep dat niet meer bestaat\"]",
                        JuistAntwoord = "Een beroep waarvoor erg moeilijk personeel te vinden is"
                    }
                }
            },

            // --- B2: AUDIO ---
            new Oefening
            {
                Titel = "Oefenen met voorzetsels (B2)",
                Niveau = OefeningNiveau.B2,
                YouTubeUrl = "https://www.youtube.com/embed/kbYWnskFz0g",
                AudioUrl = null,
                Inhoud = "Voorzetsels (preposities) zijn vaak moeilijk. In deze video-oefening test je je kennis van voorzetsels in verschillende contexten. Let goed op welk voorzetsel bij welk werkwoord hoort.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Wat is een voorbeeld van een voorzetsel?",
                        OptiesJson = "[\"Op\",\"Lopen\",\"Snel\",\"Huis\"]",
                        JuistAntwoord = "Op"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Waarom zijn voorzetsels vaak moeilijk voor taalleerders?",
                        OptiesJson = "[\"Omdat ze vaak vaste combinaties vormen met werkwoorden\",\"Omdat ze te lang zijn\",\"Omdat ze niet bestaan\",\"Omdat je ze niet hoeft te leren\"]",
                        JuistAntwoord = "Omdat ze vaak vaste combinaties vormen met werkwoorden"
                    }
                }
            },

            // ╔══════════════════════════════════════════════════════════════╗
            // ║                       C1 - GEVORDERD                        ║
            // ╚══════════════════════════════════════════════════════════════╝

            // --- C1: VIDEO ---
            new Oefening
            {
                Titel = "VRT Pano: Sociale woningen in Vlaanderen (C1)",
                Niveau = OefeningNiveau.C1,
                YouTubeUrl = "https://www.youtube.com/embed/VLAJuJMKZxs",
                Inhoud = "Kijk naar deze Pano-reportage van VRT NWS over de toewijzing van sociale woningen en mogelijke politieke inmenging. Let op de formele, journalistieke taal en de Vlaamse uitspraak.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Waarover gaat deze Pano-reportage voornamelijk?",
                        OptiesJson = "[\"De toewijzing van sociale woningen en politieke invloed\",\"De bouw van een nieuw zwembad\",\"De geschiedenis van het koningshuis\",\"Het weerbericht\"]",
                        JuistAntwoord = "De toewijzing van sociale woningen en politieke invloed"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Welk soort taalgebruik komt voor in deze documentaire?",
                        OptiesJson = "[\"Formele, journalistieke taal\",\"Informele straattaal\",\"Kindertaal\",\"Poëzie\"]",
                        JuistAntwoord = "Formele, journalistieke taal"
                    }
                }
            },

            // --- C1: TEKST ---
            new Oefening
            {
                Titel = "De diversiteit van Vlaanderen – Essay (C1)",
                Niveau = OefeningNiveau.C1,
                YouTubeUrl = null,
                AudioUrl = null,
                Inhoud = @"De Vlaamse samenleving is door de decennia heen in hoog tempo getransformeerd tot een superdiverse maatschappij. Grote steden als Antwerpen, Gent en Mechelen herbergen intussen een veelvoud aan nationaliteiten, culturen en religies. Deze evolutie is niet uitsluitend een grootstedelijk fenomeen meer; de spreiding naar centrumsteden en randgemeenten is een onmiskenbaar feit.

Het maatschappelijke debat over deze diversiteit laveert vaak tussen polariserende uitersten. Enerzijds is er het discours van verregaande asimilatie en behoud van de zogenaamde 'Vlaamse identiteit', anderzijds het pleidooi voor inclusie en meervoudig burgerschap. Voorstanders van een inclusieve benadering wijzen op de economische noodzaak van migratie om de vergrijzing op te vangen, alsook op de culturele verrijking.

Binnen dit debat staat taal – het Nederlands – steevast centraal. Het beheersen van de landstaal wordt algemeen beschouwd als de ultieme hefboom voor maatschappelijke participatie en emancipatie op de arbeidsmarkt. Het inburgeringsbeleid van de Vlaamse overheid heeft de afgelopen jaren een aanzienlijke verstrenging ondergaan. De focus ligt sterk op verplichte taalkennis en oriëntatie, met een nadruk op wederzijdse plichten.

Desalniettemin tonen academische onderzoeken aan dat uitsluitend inzetten op taalverwerving onvoldoende is. Structurele drempels, zoals discriminatie op de arbeidsmarkt en de huurmarkt, blijven hardnekkige obstakels voor een volwaardige integratie. De uitdaging voor het beleid is om niet louter in te zetten op assimilatie, maar om een gedeelde sokkel van democratische waarden te creëren waarbij ruimte blijft voor superdiversiteit.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Wat wordt volgens de tekst beschouwd als de belangrijkste hefboom voor maatschappelijke participatie?",
                        OptiesJson = "[\"Het beheersen van de Nederlandse taal\",\"Het wonen in een grote stad\",\"Het werken in de technologische sector\",\"Het hebben van een Vlaamse naam\"]",
                        JuistAntwoord = "Het beheersen van de Nederlandse taal"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Wat is volgens academisch onderzoek een blijvend obstakel voor integratie, ondanks taalkennis?",
                        OptiesJson = "[\"Structurele discriminatie op de arbeids- en huurmarkt\",\"Het slechte weer\",\"Een tekort aan supermarkten\",\"De moeilijke grammatica\"]",
                        JuistAntwoord = "Structurele discriminatie op de arbeids- en huurmarkt"
                    }
                }
            },

            // --- C1: AUDIO ---
            new Oefening
            {
                Titel = "Universiteit van Vlaanderen: Belastingen (C1)",
                Niveau = OefeningNiveau.C1,
                YouTubeUrl = "https://www.youtube.com/embed/-ZevsV1XMTI",
                AudioUrl = null,
                Inhoud = "Luister naar dit college van de Universiteit van Vlaanderen. Een professor legt uit hoe ons belastingsysteem werkt en bespreekt de vraag of belastingen onrechtvaardig zijn.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Welke ethische en economische vraag staat centraal in dit college?",
                        OptiesJson = "[\"Of het huidige belastingsysteem rechtvaardig is\",\"Hoe je belasting ontwijkt\",\"Wie de belastingen uitvond\",\"Welke bank het beste is\"]",
                        JuistAntwoord = "Of het huidige belastingsysteem rechtvaardig is"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Wie geeft deze lezing?",
                        OptiesJson = "[\"Een professor van de Universiteit van Vlaanderen\",\"Een journalist van de NOS\",\"Een middelbare scholier\",\"Een acteur\"]",
                        JuistAntwoord = "Een professor van de Universiteit van Vlaanderen"
                    }
                }
            },

            // ╔══════════════════════════════════════════════════════════════╗
            // ║                     C2 - NEAR-NATIVE                        ║
            // ╚══════════════════════════════════════════════════════════════╝

            // --- C2: VIDEO ---
            new Oefening
            {
                Titel = "VRT Pano: Buitenlandse arbeidskrachten (C2)",
                Niveau = OefeningNiveau.C2,
                YouTubeUrl = "https://www.youtube.com/embed/mItwN2q_R90",
                Inhoud = "Deze diepgravende documentaire van VRT Pano onderzoekt de uitbuiting van buitenlandse arbeidskrachten in België. Analyseer hoe de journalisten bewijzen verzamelen en de situatie in beeld brengen.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Wat is het hoofdthema van deze documentaire?",
                        OptiesJson = "[\"De uitbuiting van buitenlandse arbeidskrachten in België\",\"De voordelen van werken in het buitenland\",\"De Belgische economie\",\"Toerisme in Vlaanderen\"]",
                        JuistAntwoord = "De uitbuiting van buitenlandse arbeidskrachten in België"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Welke methode wordt gebruikt in Pano-reportages om nieuws te brengen?",
                        OptiesJson = "[\"Diepgravende onderzoeksjournalistiek\",\"Korte nieuwsflitsen\",\"Entertainment en roddels\",\"Fictieverhalen\"]",
                        JuistAntwoord = "Diepgravende onderzoeksjournalistiek"
                    }
                }
            },

            // --- C2: TEKST ---
            new Oefening
            {
                Titel = "De ethiek van kunstmatige intelligentie – Opiniestuk (C2)",
                Niveau = OefeningNiveau.C2,
                YouTubeUrl = null,
                AudioUrl = null,
                Inhoud = @"De razendsnelle ontwikkeling van kunstmatige intelligentie (AI) dwingt ons om fundamentele ethische vragen te stellen die we als samenleving niet langer kunnen negeren. De vraag is niet langer óf AI ons leven zal veranderen, maar hoe we ervoor zorgen dat deze verandering ten goede komt aan iedereen.

Een van de meest prangende kwesties is de zogenaamde 'black box'-problematiek. Veel AI-systemen, met name deep learning-modellen, nemen beslissingen die zelfs voor hun ontwikkelaars niet volledig te doorgronden zijn. Wanneer dergelijke systemen worden ingezet voor beslissingen die mensenlevens beïnvloeden – denk aan medische diagnoses, rechterlijke uitspraken of kredietbeoordelingen – rijst de vraag: wie is er verantwoordelijk als het systeem een fout maakt?

Daarnaast is er het probleem van algoritmische bias. AI-systemen leren van historische data, en als die data vooroordelen bevatten, worden die vooroordelen gereproduceerd en mogelijk zelfs versterkt. Onderzoek heeft aangetoond dat gezichtsherkenningssoftware aanzienlijk slechter presteert bij personen met een donkere huidskleur, en dat taalmodellen stereotiepe associaties kunnen versterken.

Het Europese perspectief, met de recent aangenomen AI Act, probeert een middenweg te vinden tussen innovatie en regulering. De wet classificeert AI-toepassingen naar risico en stelt strengere eisen aan systemen die als 'hoog risico' worden beschouwd. Critici menen dat deze aanpak te bureaucratisch is en innovatie remt; voorstanders zien het als een noodzakelijke stap om fundamentele rechten te beschermen.

De filosoof Hannah Arendt schreef ooit over het 'banaliteit van het kwaad' – het idee dat gewone mensen verschrikkelijke dingen kunnen doen wanneer zij ophouden zelfstandig na te denken. In het AI-tijdperk is deze waarschuwing actueler dan ooit. Het gemak waarmee we beslissingen uitbesteden aan algoritmen, zonder de onderliggende aannames te bevragen, vormt wellicht de grootste bedreiging.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Wat wordt bedoeld met de 'black box'-problematiek?",
                        OptiesJson = "[\"AI-beslissingen zijn niet volledig te doorgronden, zelfs niet voor ontwikkelaars\",\"AI werkt alleen in donkere kamers\",\"AI-systemen zijn altijd zwart van kleur\",\"Black box verwijst naar vliegtuigrecorders\"]",
                        JuistAntwoord = "AI-beslissingen zijn niet volledig te doorgronden, zelfs niet voor ontwikkelaars"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Wat is een gevolg van 'algoritmische bias' volgens de tekst?",
                        OptiesJson = "[\"Historische vooroordelen in data worden door AI gereproduceerd en versterkt\",\"Computers worden trager\",\"Mensen worden eerlijker beoordeeld\",\"AI stopt met werken\"]",
                        JuistAntwoord = "Historische vooroordelen in data worden door AI gereproduceerd en versterkt"
                    }
                }
            },

            // --- C2: AUDIO ---
            new Oefening
            {
                Titel = "Universiteit van Vlaanderen: Slaapgewoonten (C2)",
                Niveau = OefeningNiveau.C2,
                YouTubeUrl = "https://www.youtube.com/embed/jAqzBubzY_k",
                AudioUrl = null,
                Inhoud = "In dit academisch college wordt uitgelegd waarom we vaak te laat naar bed gaan. Luister naar de wetenschappelijke uitleg over slaap, ons brein en onze gewoonten.",
                IsGoedgekeurd = true,
                AangemaaktOp = DateTime.UtcNow,
                Vragen = new List<OefeningVraag>
                {
                    new OefeningVraag
                    {
                        VraagTekst = "Welk onderwerp wordt wetenschappelijk geanalyseerd in dit college?",
                        OptiesJson = "[\"Waarom we vaak te laat gaan slapen\",\"Hoe we sneller kunnen dromen\",\"De geschiedenis van het bed\",\"Waarom we vroeg moeten opstaan\"]",
                        JuistAntwoord = "Waarom we vaak te laat gaan slapen"
                    },
                    new OefeningVraag
                    {
                        VraagTekst = "Welk wetenschapsdomein is het meest relevant voor deze lezing?",
                        OptiesJson = "[\"Psychologie en neurowetenschappen\",\"Taalkunde\",\"Geschiedenis\",\"Bedrijfskunde\"]",
                        JuistAntwoord = "Psychologie en neurowetenschappen"
                    }
                }
            }
        };
    }
}
