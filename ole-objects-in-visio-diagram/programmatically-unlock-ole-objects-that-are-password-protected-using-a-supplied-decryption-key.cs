using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, decryption key
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: UnlockOleObjects <input.vsdx> <output.vsdx> <decryptionKey>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string decryptionKey = args[2];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find OLE (foreign) objects
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null)
                    {
                        // Verify that the foreign data represents an embedded OLE object
                        if (shape.ForeignData.ObjectType == ObjectType.EmbeddedObject)
                        {
                            Console.WriteLine($"Processing OLE object in shape ID {shape.ID} on page {page.ID}.");

                            byte[] encryptedData = shape.ForeignData.ObjectData;

                            // Attempt to decrypt the OLE data using the supplied key
                            byte[] decryptedData = DecryptOleData(encryptedData, decryptionKey);

                            if (decryptedData != null && decryptedData.Length > 0)
                            {
                                // Replace the encrypted data with the decrypted data
                                shape.ForeignData.ObjectData = decryptedData;
                                Console.WriteLine($"Successfully unlocked OLE object in shape ID {shape.ID}.");
                            }
                            else
                            {
                                Console.WriteLine($"Failed to decrypt OLE object in shape ID {shape.ID}. Leaving original data unchanged.");
                            }
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Decrypts OLE object data using AES-256-CBC with a key derived from the supplied password.
    /// Returns the decrypted byte array, or null if decryption fails.
    /// </summary>
    static byte[] DecryptOleData(byte[] encryptedData, string password)
    {
        try
        {
            // Derive a 256-bit key from the password using SHA-256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Use a zero IV for simplicity (replace with proper IV if known)
                byte[] iv = new byte[16];

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        return decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Decryption failed; log the error and return null
            Console.Error.WriteLine($"Decryption error: {ex.Message}");
            return null;
        }
    }
}