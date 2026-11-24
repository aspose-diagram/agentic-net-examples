using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file path, SolutionXML name, base64-encoded AES key
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <solutionXmlName> <base64Key>");
            return;
        }

        string diagramPath = args[0];
        string solutionXmlName = args[1];
        string base64Key = args[2];

        // Guard against missing diagram file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Validate key format
        byte[] keyBytes;
        try
        {
            keyBytes = Convert.FromBase64String(base64Key);
        }
        catch (FormatException)
        {
            Console.Error.WriteLine("Error: Provided key is not a valid Base64 string.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Locate the requested SolutionXML element
            SolutionXML targetXml = null;
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (string.Equals(solXml.Name, solutionXmlName, StringComparison.OrdinalIgnoreCase))
                {
                    targetXml = solXml;
                    break;
                }
            }

            if (targetXml == null)
            {
                Console.Error.WriteLine($"Error: No SolutionXML element named '{solutionXmlName}' was found.");
                return;
            }

            // The stored XmlValue is expected to be Base64-encoded encrypted data
            string encryptedBase64 = targetXml.XmlValue;
            byte[] encryptedData;
            try
            {
                encryptedData = Convert.FromBase64String(encryptedBase64);
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("Error: SolutionXML XmlValue is not a valid Base64 string.");
                return;
            }

            // Decrypt using AES (CBC) – assume first 16 bytes are IV
            if (encryptedData.Length < 16)
            {
                Console.Error.WriteLine("Error: Encrypted data is too short to contain an IV.");
                return;
            }

            byte[] iv = new byte[16];
            Array.Copy(encryptedData, 0, iv, 0, 16);
            byte[] cipherText = new byte[encryptedData.Length - 16];
            Array.Copy(encryptedData, 16, cipherText, 0, cipherText.Length);

            string decryptedXml;
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    byte[] plainBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                    decryptedXml = Encoding.UTF8.GetString(plainBytes);
                }
            }

            Console.WriteLine("Decrypted SolutionXML content:");
            Console.WriteLine(decryptedXml);

            // Verify integrity: look for a companion SolutionXML named "<name>_Hash"
            string hashElementName = solutionXmlName + "_Hash";
            string storedHashBase64 = null;
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                if (string.Equals(solXml.Name, hashElementName, StringComparison.OrdinalIgnoreCase))
                {
                    storedHashBase64 = solXml.XmlValue;
                    break;
                }
            }

            if (storedHashBase64 != null)
            {
                // Compute SHA256 of decrypted XML
                byte[] computedHash;
                using (SHA256 sha256 = SHA256.Create())
                {
                    computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(decryptedXml));
                }

                string computedHashBase64 = Convert.ToBase64String(computedHash);

                if (computedHashBase64 == storedHashBase64)
                {
                    Console.WriteLine("Integrity check passed: computed hash matches stored hash.");
                }
                else
                {
                    Console.WriteLine("Integrity check FAILED: computed hash does not match stored hash.");
                }
            }
            else
            {
                Console.WriteLine("No integrity hash element found; skipping integrity verification.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}