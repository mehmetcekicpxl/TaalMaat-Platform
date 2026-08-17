using System.Text;

namespace TaalMaat.Application.Services;

/// <summary>
/// Genereert standaard .ics-kalenderbestanden voor TaalMaat-sessie-afspraken.
/// </summary>
public class CalendarService
{
    /// <summary>
    /// Maakt een .ics-bestand als string voor een sessie
    /// </summary>
    public string GenereerIcs(string titel, DateTime begin, DateTime einde, string locatieUrl, string beschrijving)
    {
        var sb = new StringBuilder();

        sb.AppendLine("BEGIN:VCALENDAR");
        sb.AppendLine("VERSION:2.0");
        sb.AppendLine("PRODID:-//TaalMaat//Sessie//NL");
        sb.AppendLine("CALSCALE:GREGORIAN");
        sb.AppendLine("METHOD:PUBLISH");
        sb.AppendLine("BEGIN:VEVENT");
        sb.AppendLine($"UID:{Guid.NewGuid()}@taalmaat");
        sb.AppendLine($"DTSTART:{begin:yyyyMMddTHHmmss}");
        sb.AppendLine($"DTEND:{einde:yyyyMMddTHHmmss}");
        sb.AppendLine($"SUMMARY:{titel}");
        sb.AppendLine($"DESCRIPTION:{beschrijving}");
        sb.AppendLine("LOCATION:TaalMaat Platform (Online)");
        sb.AppendLine("STATUS:CONFIRMED");
        sb.AppendLine("END:VEVENT");
        sb.AppendLine("END:VCALENDAR");

        return sb.ToString();
    }

    /// <summary>
    /// Geeft de byte-array terug voor downloadbaar .ics-bestand
    /// </summary>
    public byte[] GenereerIcsBytes(string titel, DateTime begin, DateTime einde, string locatieUrl, string beschrijving) =>
        Encoding.UTF8.GetBytes(GenereerIcs(titel, begin, einde, locatieUrl, beschrijving));
}
