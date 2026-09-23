using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Aspose.Diagram;

class Program
    {
        // Simple AES decryption using a password-derived key.
        // This is a placeholder; real Office file decryption may require
        // specific libraries and formats.
        private static byte[] DecryptOleData(byte[] encryptedData, string password)
        {
            // Derive a 256‑bit key from the password.
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Use a zero IV for simplicity (replace with proper IV if known).
                byte[] iv = new byte[16];

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (MemoryStream msInput = new MemoryStream(encryptedData))
                    using (CryptoStream cryptoStream = new CryptoStream(msInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (MemoryStream msOutput = new MemoryStream())
                    {
                        cryptoStream.CopyTo(msOutput);
                        return msOutput.ToArray();
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            // Input Visio file path.
            string inputPath = "input.vsdx";

            // Output Visio file path after unlocking OLE objects.
            string outputPath = "output_unlocked.vsdx";

            // Password (decryption key) supplied by the user.
            Console.Write("Enter OLE decryption password: ");
            string password = Console.ReadLine();

            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs first to avoid modification during enumeration.
                var shapeIds = new System.Collections.Generic.List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    shapeIds.Add(shape.ID);
                }

                foreach (long shapeId in shapeIds)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Verify the shape is a foreign (OLE) shape.
                    if (shape.Type != TypeValue.Foreign)
                        continue;

                    // Ensure ForeignData and ObjectData are present.
                    if (shape.ForeignData == null || shape.ForeignData.ObjectData == null || shape.ForeignData.ObjectData.Length == 0)
                        continue;

                    // Decrypt the OLE binary data.
                    byte[] encryptedOle = shape.ForeignData.ObjectData;
                    byte[] decryptedOle;

                    try
                    {
                        decryptedOle = DecryptOleData(encryptedOle, password);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to decrypt OLE object in shape ID {shapeId}: {ex.Message}");
                        continue;
                    }

                    // Assign the decrypted data back to the shape.
                    shape.ForeignData.ObjectData = decryptedOle;
                    Console.WriteLine($"Successfully unlocked OLE object in shape ID {shapeId}.");
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }