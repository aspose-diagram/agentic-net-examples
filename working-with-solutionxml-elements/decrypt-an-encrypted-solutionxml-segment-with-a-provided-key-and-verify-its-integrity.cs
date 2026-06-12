using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Diagram;

class SolutionXmlDecryptor
{
    // Decrypts a base64‑encoded AES encrypted string using the provided key and IV.
    private static string Decrypt(string base64Cipher, byte[] key, byte[] iv)
    {
        byte[] cipherBytes = Convert.FromBase64String(base64Cipher);
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Padding = PaddingMode.PKCS7;
            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            using (MemoryStream ms = new MemoryStream(cipherBytes))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }
    }

    // Computes SHA‑256 hash of a string and returns it as a hex string.
    private static string ComputeHash(string data)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = sha.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }

    static void Main(string[] args)
    {
        try
        {

            // Input parameters (adjust as needed)
            string diagramPath = "input.vsdx";
            string outputPath = "output.vsdx";
            string solutionXmlName = "EncryptedData"; // name of the SolutionXML to process
            string keyHex = "0123456789ABCDEF0123456789ABCDEF0123456789ABCDEF0123456789ABCDEF"; // 64‑char hex (256‑bit)
            string ivHex = "ABCDEF0123456789ABCDEF0123456789"; // 32‑char hex (128‑bit)
            string expectedHash = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";

            // Convert hex strings to byte arrays
            byte[] key = new byte[keyHex.Length / 2];
            for (int i = 0; i < key.Length; i++) key[i] = Convert.ToByte(keyHex.Substring(i * 2, 2), 16);
            byte[] iv = new byte[ivHex.Length / 2];
            for (int i = 0; i < iv.Length; i++) iv[i] = Convert.ToByte(ivHex.Substring(i * 2, 2), 16);

            // Load diagram (lifecycle rule)
            Diagram diagram = new Diagram(diagramPath);

            // Find the targeted SolutionXML entry
            SolutionXML target = null;
            foreach (SolutionXML sx in diagram.SolutionXMLs)
            {
                if (sx.Name == solutionXmlName)
                {
                    target = sx;
                    break;
                }
            }

            if (target == null)
            {
                Console.WriteLine($"SolutionXML named '{solutionXmlName}' not found.");
                return;
            }

            // Decrypt the encrypted XML value
            string decryptedXml = Decrypt(target.XmlValue, key, iv);

            // Verify integrity by comparing SHA‑256 hash
            string actualHash = ComputeHash(decryptedXml);
            if (!string.Equals(actualHash, expectedHash, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Integrity verification failed: hash mismatch.");
                return;
            }

            // Replace encrypted content with decrypted XML
            target.XmlValue = decryptedXml;

            // Save the updated diagram (lifecycle rule)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Decryption succeeded and integrity verified.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
