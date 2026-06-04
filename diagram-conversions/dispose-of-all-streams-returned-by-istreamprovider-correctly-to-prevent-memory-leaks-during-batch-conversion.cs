using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace BatchHtmlExport
{
    // Implements IStreamProvider to supply streams for HTML export and ensure they are disposed.
    public class FileStreamProvider : IStreamProvider, IDisposable
    {
        // Keeps track of created streams for later disposal.
        private readonly Dictionary<string, Stream> _openStreams = new();

        // Called by Aspose.Diagram before a resource stream is needed.
        public void InitStream(StreamProviderOptions options)
        {
            // Create a file stream for the requested path.
            var stream = new FileStream(options.DefaultPath, FileMode.Create, FileAccess.Write);
            // Assign the stream back to the options so Aspose can write to it.
            options.Stream = stream;
            // Store the stream for later cleanup.
            _openStreams[options.DefaultPath] = stream;
        }

        // Called by Aspose.Diagram after the resource has been written.
        public void CloseStream(StreamProviderOptions options)
        {
            // Retrieve and dispose the stream if we have it.
            if (options.Stream != null)
            {
                options.Stream.Dispose();
                _openStreams.Remove(options.DefaultPath);
                options.Stream = null;
            }
        }

        // Ensure any remaining streams are disposed when the provider itself is disposed.
        public void Dispose()
        {
            foreach (var kvp in _openStreams)
            {
                kvp.Value.Dispose();
            }
            _openStreams.Clear();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram.
                const string diagramPath = "input.vsdx";

                // Output folder for the generated HTML files.
                const string outputFolder = "HtmlExport";

                // Ensure the output directory exists.
                Directory.CreateDirectory(outputFolder);

                // Use a using block to guarantee the Diagram is disposed.
                using (var diagram = new Diagram(diagramPath))
                {
                    // Create a single instance of the stream provider for the whole batch.
                    using (var streamProvider = new FileStreamProvider())
                    {
                        // Configure HTML save options and assign the custom stream provider.
                        var htmlOptions = new HTMLSaveOptions
                        {
                            StreamProvider = streamProvider,
                            // Optional: set other options as needed, e.g., EnlargePage = true;
                        };

                        // Iterate through all pages and shapes.
                        foreach (Page page in diagram.Pages)
                        {
                            foreach (Shape shape in page.Shapes)
                            {
                                // Skip deleted shapes.
                                if (shape.Del == BOOL.True)
                                    continue;

                                // Build a unique file name for each shape.
                                string shapeFileName = Path.Combine(
                                    outputFolder,
                                    $"Page{page.ID}_Shape{shape.ID}.html");

                                // Export the shape to HTML using the configured options.
                                shape.ToHTML(shapeFileName, htmlOptions);
                            }
                        }
                    }
                }

                Console.WriteLine("Batch HTML export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}