using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExportWithEncryption
{
    // Custom stream provider that encrypts image data before writing to disk.
    public class EncryptedStreamProvider : IStreamProvider
    {
        private readonly string _outputFolder;
        private readonly byte _xorKey = 0x5A; // simple XOR key for demonstration
        private readonly Dictionary<Stream, string> _streamPathMap = new Dictionary<Stream, string>();

        public EncryptedStreamProvider(string outputFolder)
        {
            _outputFolder = outputFolder;
            // Ensure the folder exists.
            Directory.CreateDirectory(_outputFolder);
        }

        // Called by Aspose.Diagram before writing each image.
        public void InitStream(StreamProviderOptions options)
        {
            // Create a memory stream to capture the image data.
            MemoryStream ms = new MemoryStream();
            // Assign the stream to the options so Aspose writes into it.
            options.Stream = ms;

            // Store the intended file name (DefaultPath) for later use.
            // DefaultPath is read‑only but can be read.
            string fileName = options.DefaultPath;
            _streamPathMap[ms] = Path.Combine(_outputFolder, fileName);
        }

        // Called after the image has been written to the memory stream.
        public void CloseStream(StreamProviderOptions options)
        {
            // Retrieve the memory stream that was used.
            if (options.Stream is MemoryStream ms && _streamPathMap.TryGetValue(ms, out string targetPath))
            {
                // Get the raw image bytes.
                byte[] rawData = ms.ToArray();

                // Simple XOR encryption.
                for (int i = 0; i < rawData.Length; i++)
                {
                    rawData[i] ^= _xorKey;
                }

                // Write the encrypted data to the target file.
                File.WriteAllBytes(targetPath, rawData);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram.
                string inputPath = "sample.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(inputPath);

                // Configure HTML export options.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Assign the custom stream provider to encrypt images.
                    StreamProvider = new EncryptedStreamProvider("EncryptedImages")
                };

                // Export the diagram to HTML. Images will be saved encrypted in the specified folder.
                string outputHtml = "output.html";
                diagram.Save(outputHtml, htmlOptions);

                Console.WriteLine("HTML export completed. Encrypted images are stored in the 'EncryptedImages' folder.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}