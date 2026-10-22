using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class EncryptedStreamProvider : IStreamProvider
{
    // Sample 256-bit key and 128-bit IV for AES encryption.
    private static readonly byte[] _key = new byte[32] {
        0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
        0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
        0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
        0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F };
    private static readonly byte[] _iv = new byte[16] {
        0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7,
        0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF };

    // Called by Aspose.Diagram before writing a resource (e.g., an image) to a stream.
    public void InitStream(StreamProviderOptions options)
    {
        // Determine the target file path for the resource.
        string targetPath = options.DefaultPath;

        // Ensure the directory exists.
        string directory = Path.GetDirectoryName(targetPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a file stream that will receive the encrypted data.
        FileStream fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write);

        // Set up AES encryption.
        Aes aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        // Wrap the file stream with a CryptoStream for encryption.
        CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

        // Assign the encrypted stream back to the options so Aspose writes into it.
        options.Stream = cryptoStream;
    }

    // Called by Aspose.Diagram after the resource has been written.
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            // Flush and close the CryptoStream (which also closes the underlying FileStream).
            options.Stream.Flush();
            options.Stream.Close();
            options.Stream = null;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard against missing output folder (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare HTML save options with a custom stream provider.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            htmlOptions.StreamProvider = new EncryptedStreamProvider();

            // Define the output HTML file path.
            string htmlOutputPath = Path.Combine(outputFolder, "output.html");

            // Save the diagram to HTML; images will be written via the encrypted stream provider.
            diagram.Save(htmlOutputPath, htmlOptions);

            Console.WriteLine($"Diagram successfully saved to HTML at: {htmlOutputPath}");
            Console.WriteLine("Embedded images have been encrypted on disk.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}