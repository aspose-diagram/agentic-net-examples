using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Diagram;

class SolutionXmlDecryptor
{
    // Decrypts a base64‑encoded AES cipher text using the provided key.
    private static string DecryptString(string cipherTextBase64, string key)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherTextBase64);
        // Derive a 256‑bit key from the supplied key string.
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] keyBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            // Use a zero IV for simplicity (must match the encryption side).
            byte[] iv = new byte[16];
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
        }
    }

    // Computes SHA‑256 hash of a string and returns it as a hex string.
    private static string ComputeHash(string data)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }

    static void Main(string[] args)
    {
        try
        {

            // Input parameters.
            string diagramPath = @"C:\Diagrams\input.vsdx";   // Path to the Visio file.
            string outputPath = @"C:\Diagrams\output.vsdx";   // Path where the modified file will be saved.
            string solutionXmlName = "EncryptedData";        // Name of the SolutionXML that holds the encrypted XML.
            string key = "YourSecretKey";                    // Decryption key supplied by the caller.

            // Load the diagram (uses Aspose.Diagram's load rule).
            Diagram diagram = new Diagram(diagramPath);

            // Locate the encrypted SolutionXML entry.
            SolutionXML encryptedXml = null;
            foreach (SolutionXML sx in diagram.SolutionXMLs)
            {
                if (sx.Name == solutionXmlName)
                {
                    encryptedXml = sx;
                    break;
                }
            }

            if (encryptedXml == null)
            {
                Console.WriteLine($"SolutionXML with name '{solutionXmlName}' not found.");
                return;
            }

            // Decrypt the XML value.
            string decryptedXml = DecryptString(encryptedXml.XmlValue, key);
            Console.WriteLine("Decryption successful.");

            // Verify integrity.
            // Expect a companion SolutionXML named "<name>_Hash" that stores the SHA‑256 hash of the original plain XML.
            string hashXmlName = solutionXmlName + "_Hash";
            string storedHash = null;
            foreach (SolutionXML sx in diagram.SolutionXMLs)
            {
                if (sx.Name == hashXmlName)
                {
                    storedHash = sx.XmlValue;
                    break;
                }
            }

            if (storedHash != null)
            {
                string computedHash = ComputeHash(decryptedXml);
                if (string.Equals(computedHash, storedHash, StringComparison.OrdinalIgnoreCase))
                    Console.WriteLine("Integrity check passed.");
                else
                    Console.WriteLine("Integrity check failed: hash mismatch.");
            }
            else
            {
                Console.WriteLine("No hash entry found; skipping integrity verification.");
            }

            // Optionally replace the encrypted entry with the decrypted XML.
            encryptedXml.XmlValue = decryptedXml;
            encryptedXml.Name = solutionXmlName + "_Decrypted";

            // Save the modified diagram (uses Aspose.Diagram's save rule).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
