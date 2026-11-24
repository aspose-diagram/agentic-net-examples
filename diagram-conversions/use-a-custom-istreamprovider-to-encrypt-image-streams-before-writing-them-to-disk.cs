using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output HTML file path
        string outputHtmlPath = args.Length > 1 ? args[1] : "output.html";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure HTML save options with a custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new EncryptingStreamProvider();

                // Save diagram to HTML using the configured options
                diagram.Save(outputHtmlPath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}

// Custom stream provider that encrypts image streams before writing them to disk
class EncryptingStreamProvider : IStreamProvider
{
    // Called by Aspose.Diagram before writing each image stream
    public void InitStream(StreamProviderOptions options)
    {
        // Use a memory stream to capture the raw image data
        options.Stream = new MemoryStream();
    }

    // Called after the image data has been written to the stream
    public void CloseStream(StreamProviderOptions options)
    {
        // Retrieve the captured image bytes
        if (options.Stream is not MemoryStream memoryStream)
            return;

        byte[] plainData = memoryStream.ToArray();

        // Simple AES encryption (256-bit key, 128-bit IV)
        byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF"); // 32 bytes
        byte[] iv = Encoding.UTF8.GetBytes("0123456789ABCDEF"); // 16 bytes

        byte[] encryptedData;
        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            using (MemoryStream encryptedStream = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainData, 0, plainData.Length);
                cryptoStream.FlushFinalBlock();
                encryptedData = encryptedStream.ToArray();
            }
        }

        // Determine the target file path (use the default path provided by Aspose)
        string targetPath = options.DefaultPath;

        // Ensure the directory exists
        string directory = Path.GetDirectoryName(targetPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Write the encrypted bytes to the target file
        File.WriteAllBytes(targetPath, encryptedData);
    }
}