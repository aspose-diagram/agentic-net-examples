using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Diagram;

class UnlockOleObjects
{
    static void Main()
    {
        try
        {

            // Input Visio file containing password‑protected OLE objects
            string inputPath = "input.vsdx";

            // Output Visio file with OLE objects unlocked
            string outputPath = "output_unlocked.vsdx";

            // Decryption key (password) supplied by the user
            string decryptionKey = "YourDecryptionPassword";

            // Load the diagram (use the provided load rule)
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find OLE objects
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // ForeignData holds OLE data; check if it exists and contains embedded data
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                    {
                        // Decrypt the OLE data using the supplied password
                        byte[] decryptedData = DecryptOleData(shape.ForeignData.ObjectData, decryptionKey);

                        // Replace the encrypted data with the decrypted data
                        shape.ForeignData.ObjectData = decryptedData;
                    }
                }
            }

            // Save the modified diagram (use the provided save rule)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Decrypts OLE data assuming it was encrypted with AES using a key derived from the password.
    private static byte[] DecryptOleData(byte[] encryptedData, string password)
    {
        // Derive a 256‑bit key from the password (simple SHA‑256 hash for demonstration)
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] key = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            // Use the first 16 bytes of the hash as IV (example; actual IV must match encryption)
            byte[] iv = new byte[16];
            Array.Copy(key, iv, iv.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream output = new MemoryStream())
                using (CryptoStream crypto = new CryptoStream(output, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    crypto.Write(encryptedData, 0, encryptedData.Length);
                    crypto.FlushFinalBlock();
                    return output.ToArray();
                }
            }
        }
    }
}
