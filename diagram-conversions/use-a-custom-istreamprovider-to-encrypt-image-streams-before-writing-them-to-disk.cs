using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that encrypts each resource stream before writing to disk.
    public class EncryptedStreamProvider : IStreamProvider
    {
        // Example AES key and IV (16 bytes each for AES-128). In real scenarios, use secure key management.
        private readonly byte[] _key = new byte[16] { 0x10, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF };
        private readonly byte[] _iv  = new byte[16] { 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10, 0xEF, 0xCD, 0xAB, 0x89, 0x67, 0x45, 0x23, 0x01 };

        public void InitStream(StreamProviderOptions options)
        {
            // Determine a safe output path for the encrypted resource.
            string fileName = Path.GetFileName(options.DefaultPath);
            string encryptedDir = Path.Combine("encrypted_resources");
            Directory.CreateDirectory(encryptedDir);
            string encryptedPath = Path.Combine(encryptedDir, "enc_" + fileName);

            // Create a file stream that will receive encrypted data.
            FileStream fileStream = new FileStream(encryptedPath, FileMode.Create, FileAccess.Write);

            // Set up AES encryption.
            Aes aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            // Wrap the file stream with a CryptoStream for encryption.
            CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            // Assign the encrypted stream back to the options so Aspose writes into it.
            options.Stream = cryptoStream;
        }

        public void CloseStream(StreamProviderOptions options)
        {
            // Ensure the CryptoStream (and underlying file stream) are properly closed.
            options.Stream?.Close();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Configure HTML export options with the custom encrypted stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new EncryptedStreamProvider();

                // Export the diagram to HTML; resources will be written encrypted.
                diagram.Save("output.html", htmlOptions);

                Console.WriteLine("HTML export completed. Encrypted resources are stored in the 'encrypted_resources' folder.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}