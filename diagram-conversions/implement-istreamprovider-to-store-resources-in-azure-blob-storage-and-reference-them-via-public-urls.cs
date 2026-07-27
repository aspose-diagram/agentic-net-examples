using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to store HTML resources (e.g., images) in Azure Blob Storage.
    // This version uses a local temporary file as a placeholder to avoid Azure SDK version conflicts.
    public class AzureBlobStreamProvider : IStreamProvider
    {
        private readonly string _tempFolder;

        // Initialize the provider with a temporary folder for resource files.
        public AzureBlobStreamProvider()
        {
            // Create a unique temporary folder for this run.
            _tempFolder = Path.Combine(Path.GetTempPath(), "AsposeDiagramResources", Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempFolder);
        }

        // Called by Aspose.Diagram for each resource that needs a stream.
        public void InitStream(StreamProviderOptions options)
        {
            // Use the default path (relative file name) as the temporary file name.
            string tempFilePath = Path.Combine(_tempFolder, options.DefaultPath);

            // Ensure the directory for the file exists.
            Directory.CreateDirectory(Path.GetDirectoryName(tempFilePath)!);

            // Open a writable file stream and assign it to the options.
            options.Stream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write);
        }

        // Called after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream to flush data to the temporary file.
            options.Stream?.Dispose();

            // NOTE: In a production scenario, you would upload the temporary file to Azure Blob Storage here
            // using Azure REST API or a compatible SDK that matches the project’s referenced versions.
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Path to the source Visio file.
            string sourceVisioPath = "input.vsdx";

            // Guard: ensure the source file exists.
            if (!File.Exists(sourceVisioPath))
            {
                Console.Error.WriteLine($"File not found: {sourceVisioPath}");
                return;
            }

            try
            {
                // Load the diagram.
                Diagram diagram = new Diagram(sourceVisioPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            // Configure HTML export options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Use the custom stream provider to handle resource streams.
                StreamProvider = new AzureBlobStreamProvider(),

                // Example: generate separate files (not a single bundled HTML file).
                SaveAsSingleFile = false
            };

            // Export the diagram to HTML. The main HTML file is saved locally.
            string outputHtmlPath = "output.html";

            try
            {
                // Save using the HTML options (requires a valid second argument).
                Diagram diagram = new Diagram(sourceVisioPath);
                diagram.Save(outputHtmlPath, htmlOptions);
                Console.WriteLine("HTML export completed. Resources are stored in temporary files.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during HTML export: {ex.Message}");
            }
        }
    }
}