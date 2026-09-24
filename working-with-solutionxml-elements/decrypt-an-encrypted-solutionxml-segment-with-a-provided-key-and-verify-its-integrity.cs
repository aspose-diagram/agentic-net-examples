using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using Aspose.Diagram;

class Program
{
    // Decrypts AES-CBC encrypted data. The first 16 bytes of the cipher are treated as the IV.
    static string DecryptAesCbc(byte[] cipherBytes, byte[] keyBytes)
    {
        if (cipherBytes.Length < 16)
            throw new Exception("Cipher data is too short to contain an IV.");

        byte[] iv = cipherBytes.Take(16).ToArray();
        byte[] actualCipher = cipherBytes.Skip(16).ToArray();

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            using (MemoryStream ms = new MemoryStream(actualCipher))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }
    }

    // Computes SHA256 hash of a string and returns it as a hex string.
    static string ComputeSha256(string text)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = sha.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }

    static void Main()
    {
        try
        {

            // Path to the Visio file containing the encrypted SolutionXML.
            string diagramPath = "input.vsdx";

            // 32‑byte (256‑bit) key for AES decryption. Replace with the actual key.
            string keyString = "0123456789ABCDEF0123456789ABCDEF";
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);
            if (keyBytes.Length != 32)
                throw new Exception("The decryption key must be 32 bytes for AES‑256.");

            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Locate the encrypted SolutionXML element.
            SolutionXML encryptedXml = null;
            foreach (SolutionXML s in diagram.SolutionXMLs)
            {
                if (s.Name == "EncryptedData")
                {
                    encryptedXml = s;
                    break;
                }
            }

            if (encryptedXml == null)
                throw new Exception("Encrypted SolutionXML element not found.");

            // The encrypted content is expected to be Base64‑encoded.
            byte[] cipherBytes = Convert.FromBase64String(encryptedXml.XmlValue);

            // Decrypt the content.
            string decryptedXml = DecryptAesCbc(cipherBytes, keyBytes);
            Console.WriteLine("Decrypted XML:");
            Console.WriteLine(decryptedXml);

            // Verify integrity by comparing SHA256 hash with a stored hash element.
            string storedHash = null;
            foreach (SolutionXML s in diagram.SolutionXMLs)
            {
                if (s.Name == "EncryptedDataHash")
                {
                    storedHash = s.XmlValue.Trim();
                    break;
                }
            }

            if (storedHash == null)
                throw new Exception("Hash for integrity verification not found.");

            string computedHash = ComputeSha256(decryptedXml);
            Console.WriteLine($"Computed SHA256: {computedHash}");
            Console.WriteLine($"Stored   SHA256: {storedHash}");

            if (string.Equals(computedHash, storedHash, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Integrity check passed: the decrypted XML matches the stored hash.");
            }
            else
            {
                throw new Exception("Integrity check failed: the decrypted XML does not match the stored hash.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
