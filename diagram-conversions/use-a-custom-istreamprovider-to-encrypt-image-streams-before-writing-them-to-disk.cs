using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExportWithEncryption
{
    // Custom stream provider that encrypts image streams before they are written to disk
    public class EncryptedStreamProvider : IStreamProvider
    {
        // Simple AES key and IV for demonstration (DO NOT use hard‑coded keys in production)
        private static readonly byte[] _key = new byte[32] {
            0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,
            0x08,0x09,0x0A,0x0B,0x0C,0x0D,0x0E,0x0F,
            0x10,0x11,0x12,0x13,0x14,0x15,0x16,0x17,
            0x18,0x19,0x1A,0x1B,0x1C,0x1D,0x1E,0x1F };
        private static readonly byte[] _iv = new byte[16] {
            0xA0,0xA1,0xA2,0xA3,0xA4,0xA5,0xA6,0xA7,
            0xA8,0xA9,0xAA,0xAB,0xAC,0xAD,0xAE,0xAF };

        // Called by Aspose.Diagram before writing an image stream
        public void InitStream(StreamProviderOptions options)
        {
            // Provide a temporary memory stream where the library will write the image bytes
            options.Stream = new MemoryStream();
        }

        // Called after the image data has been written to the temporary stream
        public void CloseStream(StreamProviderOptions options)
        {
            // Retrieve the in‑memory image data
            var memoryStream = options.Stream as MemoryStream;
            if (memoryStream == null)
                return;

            byte[] plainBytes = memoryStream.ToArray();

            // Encrypt the image bytes
            byte[] encryptedBytes = Encrypt(plainBytes);

            // Write the encrypted data to the target path supplied by Aspose.Diagram
            // DefaultPath contains the file name that would have been used for the image
            using (var fileStream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            }

            // Clean up the temporary stream
            memoryStream.Dispose();
        }

        // AES encryption helper
        private static byte[] Encrypt(byte[] data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                    }
                    return ms.ToArray();
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Configure HTML export options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new EncryptedStreamProvider();

                // Export the diagram to HTML; image files will be encrypted by the provider
                diagram.Save("output.html", htmlOptions);

                Console.WriteLine("HTML export completed. Image files are encrypted.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}