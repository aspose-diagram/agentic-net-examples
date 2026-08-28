using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to write external resources (e.g., images) to a local folder.
    // This avoids Azure SDK version conflicts while satisfying the IStreamProvider contract.
    public class AzureBlobStreamProvider : IStreamProvider
    {
        private readonly string _outputFolder;

        // Constructor receives a folder path where resources will be stored.
        public AzureBlobStreamProvider(string outputFolder)
        {
            // Ensure the target folder exists.
            _outputFolder = outputFolder;
            if (!Directory.Exists(_outputFolder))
            {
                Directory.CreateDirectory(_outputFolder);
            }
        }

        // Called by Aspose.Diagram when a resource stream is required.
        public void InitStream(StreamProviderOptions options)
        {
            // Extract the file name from the default path (e.g., "image1.png").
            string fileName = Path.GetFileName(options.DefaultPath);
            // Combine with the output folder to get the full local path.
            string fullPath = Path.Combine(_outputFolder, fileName);
            // Open a writable file stream (overwrite if it already exists).
            Stream fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            // Assign the stream so Aspose writes directly to the file.
            options.Stream = fileStream;
        }

        // Called after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream to finalize the write operation.
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            // Local folder to simulate Azure Blob container for resource files.
            const string resourceFolder = "blobResources";

            // Path to the source Visio diagram.
            const string diagramPath = "input.vsdx";

            // Guard: ensure the diagram file exists before proceeding.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                return;
            }

            try
            {
                // Load the diagram from the specified file.
                Diagram diagram = new Diagram(diagramPath);

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // The provider will write external resources to the local folder.
                    StreamProvider = new AzureBlobStreamProvider(resourceFolder)
                };

                // Export the diagram to HTML. Resources (images, etc.) will be stored in the folder.
                const string outputHtmlPath = "output.html";
                diagram.Save(outputHtmlPath, htmlOptions);

                Console.WriteLine("Diagram exported to HTML. Resources are stored in the local folder.");
            }
            catch (Exception ex)
            {
                // Write any Aspose or I/O errors to the error console.
                Console.Error.WriteLine($"Error during export: {ex.Message}");
            }
        }
    }
}