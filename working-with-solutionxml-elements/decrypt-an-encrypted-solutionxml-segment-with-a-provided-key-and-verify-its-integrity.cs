using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file path, SolutionXML name, Base64-encoded AES key
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <solutionXmlName> <base64Key>");
            return;
        }

        string diagramPath = args[0];
        string solutionXmlName = args[1];
        string base64Key = args[2];

        // Guard against missing diagram file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Validate the provided key
        byte[] keyBytes;
        try
        {
            keyBytes = Convert.FromBase64String(base64Key);
        }
        catch (FormatException)
        {
            Console.Error.WriteLine("The provided key is not a valid Base64 string.");
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
                if (solXml.Name == solutionXmlName)
                {
                    targetXml = solXml;
                    break;
                }
            }

            if (targetXml == null)
            {
                Console.Error.WriteLine($"SolutionXML with name '{solutionXmlName}' not found.");
                return;
            }

            // The XmlValue is expected to be Base64-encoded ciphertext (IV + Cipher)
            byte[] encryptedData;
            try
            {
                encryptedData = Convert.FromBase64String(targetXml.XmlValue);
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("The SolutionXML XmlValue is not a valid Base64 string.");
                return;
            }

            if (encryptedData.Length < 16)
            {
                Console.Error.WriteLine("Encrypted data is too short to contain an IV.");
                return;
            }

            // Extract IV (first 16 bytes) and ciphertext (remaining bytes)
            byte[] iv = new byte[16];
            Array.Copy(encryptedData, 0, iv, 0, 16);
            byte[] cipherText = new byte[encryptedData.Length - 16];
            Array.Copy(encryptedData, 16, cipherText, 0, cipherText.Length);

            // Decrypt using AES CBC with PKCS7 padding
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

            // Output the decrypted XML
            Console.WriteLine("Decrypted SolutionXML:");
            Console.WriteLine(decryptedXml);

            // Compute SHA256 hash of the decrypted content for integrity verification
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(decryptedXml));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                Console.WriteLine($"SHA256 Hash: {sb}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}