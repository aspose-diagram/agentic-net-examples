using System;
using System.Diagnostics;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace HtmlConversionPerformance
{
    // Custom stream provider that uses a MemoryStream for exported objects
    class MemoryStreamProvider : IStreamProvider
    {
        // Called by Aspose.Diagram to obtain a stream for a resource
        public void InitStream(StreamProviderOptions options)
        {
            // Provide a fresh MemoryStream
            options.Stream = new MemoryStream();
        }

        // Called by Aspose.Diagram after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream to release resources
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source VSDX file
                string sourceFile = @"C:\Diagrams\sample.vsdx";

                // Load the diagram (lifecycle rule: use provided load mechanism)
                Diagram diagram = new Diagram(sourceFile);

                // Select a shape to convert to HTML (using the first shape on the first page)
                Shape shape = diagram.Pages[0].Shapes[0];

                // -----------------------------------------------------------------
                // Conversion without a custom IStreamProvider (default behavior)
                // -----------------------------------------------------------------
                HTMLSaveOptions defaultOptions = new HTMLSaveOptions();

                Stopwatch swDefault = Stopwatch.StartNew();
                // The ToHTML method writes the HTML file and uses default internal streams
                shape.ToHTML(@"C:\Output\default.html", defaultOptions);
                swDefault.Stop();

                Console.WriteLine($"Conversion without custom IStreamProvider: {swDefault.ElapsedMilliseconds} ms");

                // -----------------------------------------------------------------
                // Conversion with a custom IStreamProvider (MemoryStreamProvider)
                // -----------------------------------------------------------------
                HTMLSaveOptions customOptions = new HTMLSaveOptions
                {
                    // Assign the custom stream provider
                    StreamProvider = new MemoryStreamProvider()
                };

                Stopwatch swCustom = Stopwatch.StartNew();
                // The same ToHTML call now routes exported resources through the custom provider
                shape.ToHTML(@"C:\Output\custom.html", customOptions);
                swCustom.Stop();

                Console.WriteLine($"Conversion with custom IStreamProvider: {swCustom.ElapsedMilliseconds} ms");

                // Clean up
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}