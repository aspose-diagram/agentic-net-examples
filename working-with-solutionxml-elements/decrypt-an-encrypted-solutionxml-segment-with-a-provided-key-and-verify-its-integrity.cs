using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Diagram;

class SolutionXmlDecryptor
{
    // Decrypts a base64‑encoded AES‑CBC encrypted string using the supplied key and IV.
    private static string DecryptString(string encryptedBase64, byte[] key, byte[] iv)
    {
        byte[] cipherBytes = Convert.FromBase64String(encryptedBase64);
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
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

    // Computes a SHA256 hash of the supplied text and returns it as a hex string.
    private static string ComputeSha256Hash(string text)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }

    // Main routine: loads a diagram, decrypts each SolutionXML entry, verifies integrity,
    // and saves the diagram with the decrypted XML values.
    public static void DecryptSolutionXml(string diagramPath, string outputPath, byte[] key, byte[] iv)
    {
        // Load the diagram (Aspose.Diagram handles the lifecycle internally).
        Diagram diagram = new Diagram(diagramPath);

        // Iterate over all SolutionXML objects stored in the diagram.
        foreach (SolutionXML solXml in diagram.SolutionXMLs)
        {
            // Assume the encrypted data is stored in XmlValue.
            string encryptedData = solXml.XmlValue;

            // Decrypt the XML content.
            string decryptedXml = DecryptString(encryptedData, key, iv);

            // Optional integrity check:
            // Assume the original XML contains a <Hash> element with a SHA256 hash of the clear XML.
            // Extract the hash from the decrypted XML (simple example, real XML parsing recommended).
            string expectedHash = null;
            int hashStart = decryptedXml.IndexOf("<Hash>", StringComparison.Ordinal);
            int hashEnd = decryptedXml.IndexOf("</Hash>", StringComparison.Ordinal);
            if (hashStart != -1 && hashEnd != -1 && hashEnd > hashStart)
            {
                expectedHash = decryptedXml.Substring(hashStart + 6, hashEnd - (hashStart + 6)).Trim();
                // Remove the <Hash> element to obtain the actual payload.
                decryptedXml = decryptedXml.Remove(hashStart, (hashEnd + 7) - hashStart);
            }

            // Compute hash of the payload.
            string actualHash = ComputeSha256Hash(decryptedXml);

            // Verify integrity if a hash was present.
            if (expectedHash != null && !string.Equals(expectedHash, actualHash, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException($"Integrity check failed for SolutionXML named '{solXml.Name}'.");
            }

            // Replace the encrypted value with the decrypted XML.
            solXml.XmlValue = decryptedXml;
        }

        // Save the modified diagram.
        diagram.Save(outputPath, SaveFileFormat.Vdx);
    }

    // Example usage.
    static void Main()
    {
        try
        {

            // Path to the diagram containing encrypted SolutionXML.
            string inputDiagram = @"C:\Diagrams\encrypted.vdx";

            // Path where the diagram with decrypted XML will be saved.
            string outputDiagram = @"C:\Diagrams\decrypted.vdx";

            // Example 256‑bit key and 128‑bit IV (must match the encryption parameters).
            byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF"); // 32 bytes
            byte[] iv  = Encoding.UTF8.GetBytes("ABCDEF0123456789");               // 16 bytes

            DecryptSolutionXml(inputDiagram, outputDiagram, key, iv);
            Console.WriteLine("Decryption completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
