using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider that supplies resource streams for HTML export.
    // Throws an exception when the requested resource cannot be found.
    public class CustomStreamProvider : IStreamProvider
    {
        private readonly string _resourceBasePath;

        public CustomStreamProvider(string resourceBasePath)
        {
            _resourceBasePath = resourceBasePath;
        }

        // Called by Aspose.Diagram when a resource stream is required.
        public void InitStream(StreamProviderOptions options)
        {
            // The name of the resource to be provided (e.g., image file name).
            string resourceName = options.DefaultPath;

            // Build the full path to the resource file.
            string fullPath = Path.Combine(_resourceBasePath, resourceName);

            if (File.Exists(fullPath))
            {
                // Assign the opened stream to the options so Aspose can read it.
                options.Stream = File.OpenRead(fullPath);
            }
            else
            {
                // Resource is missing – handle the error as required.
                // Here we throw an exception to stop the export and inform the caller.
                throw new FileNotFoundException($"Required resource not found: {fullPath}");
            }
        }

        // Called after Aspose.Diagram finishes using the stream.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created.
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    // Folder where external resources (images, CSS, etc.) are stored.
                    StreamProvider = new CustomStreamProvider("Resources")
                };

                // Export the diagram to HTML. Missing resources will cause an exception.
                diagram.Save("output.html", htmlOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}