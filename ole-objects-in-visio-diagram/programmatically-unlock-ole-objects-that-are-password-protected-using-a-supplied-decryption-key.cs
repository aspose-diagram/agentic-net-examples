using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Diagram;

class UnlockOleObjects
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output_unlocked.vsdx";

            // Password used to protect the OLE objects
            string olePassword = "YourOlePassword";

            // Load the Visio diagram (lifecycle rule: use provided load method)
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find OLE objects
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // ForeignData holds embedded or linked OLE data
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                    {
                        // Attempt to decrypt the OLE data using the supplied password
                        byte[] decryptedData = DecryptOleData(shape.ForeignData.ObjectData, olePassword);

                        // If decryption succeeded, replace the encrypted blob with the decrypted one
                        if (decryptedData != null && decryptedData.Length > 0 && !AreArraysEqual(decryptedData, shape.ForeignData.ObjectData))
                        {
                            shape.ForeignData.ObjectData = decryptedData;
                        }
                    }
                }
            }

            // Save the modified diagram (lifecycle rule: use provided save method)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Decrypts OLE data encrypted with a simple AES scheme.
    // Replace this implementation with the actual algorithm used for the OLE protection if different.
    private static byte[] DecryptOleData(byte[] encryptedData, string password)
    {
        try
        {
            // Derive a 256‑bit key and a 128‑bit IV from the password
            using (SHA256 sha256 = SHA256.Create())
            using (MD5 md5 = MD5.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedData, 0, encryptedData.Length);
                        cs.FlushFinalBlock();
                        return ms.ToArray();
                    }
                }
            }
        }
        catch
        {
            // If decryption fails, return the original data unchanged
            return encryptedData;
        }
    }

    // Helper to compare two byte arrays
    private static bool AreArraysEqual(byte[] a1, byte[] a2)
    {
        if (a1 == null || a2 == null) return false;
        if (a1.Length != a2.Length) return false;
        for (int i = 0; i < a1.Length; i++)
            if (a1[i] != a2[i]) return false;
        return true;
    }
}
