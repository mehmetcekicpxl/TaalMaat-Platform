using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TaalMaat.Application.Services;

/// <summary>
/// Service voor het versleutelen (AES) van gevoelige data zoals chatberichten.
/// Aansluitend op de kennis uit het CryptoTool project.
/// </summary>
public class EncryptieService
{
    private readonly byte[] _key;

    public EncryptieService(IConfiguration configuration)
    {
        var keyString = configuration["Security:AesKey"];
        if (string.IsNullOrEmpty(keyString) || keyString.Length != 32)
        {
            throw new InvalidOperationException("Geldige 32-karakter AES key ontbreekt in appsettings.json (Security:AesKey).");
        }
        _key = Encoding.UTF8.GetBytes(keyString);
    }

    public string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText)) return plainText;

        using var aes = Aes.Create();
        aes.Key = _key;
        aes.GenerateIV();
        var iv = aes.IV;

        using var encryptor = aes.CreateEncryptor(aes.Key, iv);
        using var ms = new MemoryStream();
        
        // Bewaar de IV (Initialization Vector) aan het begin van de cipher,
        // deze is nodig voor decrypteren en hoeft niet geheim te zijn.
        ms.Write(iv, 0, iv.Length); 
        
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText)) return cipherText;

        try
        {
            var fullCipher = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            aes.Key = _key;

            var ivLength = aes.BlockSize / 8;
            var iv = new byte[ivLength];
            Array.Copy(fullCipher, 0, iv, 0, ivLength);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(fullCipher, ivLength, fullCipher.Length - ivLength);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            
            return sr.ReadToEnd();
        }
        catch
        {
            // Als decryptie faalt (bijv. oude onversleutelde testdata), stuur origineel terug
            return cipherText;
        }
    }
}
