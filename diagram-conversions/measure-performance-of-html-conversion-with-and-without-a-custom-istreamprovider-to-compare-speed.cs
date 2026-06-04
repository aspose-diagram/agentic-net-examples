using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace HtmlConversionPerformance
{
    // Custom stream provider for HTML export
    public class MyStreamProvider : IStreamProvider
    {
        // Called before each resource stream is created
        public void InitStream(StreamProviderOptions options)
        {
            // Use a memory stream for the resource
            options.Stream = new MemoryStream();
            // Optionally set a custom base path for resources (if needed)
            // options.CustomPath = "resources/";
        }

        // Called after the resource stream is closed
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the memory stream if it was created
            if (options.Stream != null)
            {
                options.Stream.Dispose();
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Paths (replace with actual file locations)
                string inputDiagramPath = "input.vsdx";
                string outputHtmlNoProvider = "output_no_provider.html";
                string outputHtmlWithProvider = "output_with_provider.html";

                // Load the diagram
                Diagram diagram = new Diagram(inputDiagramPath);

                // ------------------------------
                // Export without custom IStreamProvider
                // ------------------------------
                HTMLSaveOptions optionsNoProvider = new HTMLSaveOptions();
                // No StreamProvider assigned – default behavior

                Stopwatch swNoProvider = Stopwatch.StartNew();
                diagram.Save(outputHtmlNoProvider, optionsNoProvider);
                swNoProvider.Stop();

                Console.WriteLine($"HTML export without IStreamProvider took: {swNoProvider.ElapsedMilliseconds} ms");

                // ------------------------------
                // Export with custom IStreamProvider
                // ------------------------------
                HTMLSaveOptions optionsWithProvider = new HTMLSaveOptions
                {
                    StreamProvider = new MyStreamProvider()
                };

                Stopwatch swWithProvider = Stopwatch.StartNew();
                diagram.Save(outputHtmlWithProvider, optionsWithProvider);
                swWithProvider.Stop();

                Console.WriteLine($"HTML export with IStreamProvider took: {swWithProvider.ElapsedMilliseconds} ms");

                // Simple comparison output
                if (swWithProvider.ElapsedMilliseconds < swNoProvider.ElapsedMilliseconds)
                {
                    Console.WriteLine("Custom IStreamProvider improved performance.");
                }
                else if (swWithProvider.ElapsedMilliseconds > swNoProvider.ElapsedMilliseconds)
                {
                    Console.WriteLine("Custom IStreamProvider slowed down the export.");
                }
                else
                {
                    Console.WriteLine("Performance is identical.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}