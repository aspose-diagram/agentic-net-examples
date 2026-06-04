using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlEncryption
{
    // Custom stream provider that encrypts image streams before they are written to disk.
    public class EncryptingStreamProvider : IStreamProvider
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public EncryptingStreamProvider(byte[] key, byte[] iv)
        {
            _key = key;
            _iv = iv;
        }

        // Called by Aspose.Diagram when it needs a stream for an image resource.
        public void InitStream(StreamProviderOptions options)
        {
            // The DefaultPath property contains the relative file name for the image.
            // Create a file stream for that path and wrap it with a CryptoStream for encryption.
            var fileStream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;
            var encryptor = aes.CreateEncryptor();
            var cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write);
            // Assign the encrypted stream back to the options so Aspose writes into it.
            options.Stream = cryptoStream;
        }

        // Called after the image has been written; clean up resources.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the CryptoStream (which also disposes the underlying FileStream).
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram.
                const string inputPath = "input.vsdx";
                // Path where the HTML output will be generated.
                const string outputPath = "output.html";

                // Load the diagram.
                using var diagram = new Diagram(inputPath);

                // Prepare HTML save options.
                var htmlOptions = new HTMLSaveOptions();

                // Example AES key and IV (must be 16, 24, or 32 bytes for key; 16 bytes for IV).
                // In a real scenario, retrieve these securely.
                byte[] key = new byte[32]; // 256‑bit key
                byte[] iv = new byte[16];  // 128‑bit IV
                // For demonstration, fill with zeros (not secure).
                Array.Clear(key, 0, key.Length);
                Array.Clear(iv, 0, iv.Length);

                // Assign the custom encrypting stream provider.
                htmlOptions.StreamProvider = new EncryptingStreamProvider(key, iv);

                // Save the diagram as HTML; images will be encrypted by the provider.
                diagram.Save(outputPath, htmlOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}