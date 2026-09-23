using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Implements IStreamProvider to supply resource streams for HTML export.
    // The stream contains the raw bytes of the resource (e.g., an image file).
    // Aspose.Diagram will embed these bytes as base64 data URIs in the generated HTML.
    public class Base64StreamProvider : IStreamProvider
    {
        public void InitStream(StreamProviderOptions options)
        {
            // options.DefaultPath holds the relative path of the requested resource.
            // Attempt to load the resource from the file system; if not found, provide an empty stream.
            string resourcePath = options.DefaultPath;

            if (File.Exists(resourcePath))
            {
                byte[] data = File.ReadAllBytes(resourcePath);
                options.Stream = new MemoryStream(data);
            }
            else
            {
                // Provide an empty stream to avoid null reference issues.
                options.Stream = new MemoryStream();
            }
        }

        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream after Aspose.Diagram finishes using it.
            options.Stream?.Dispose();
        }
    }

    public class Program
    {
        // Entry point.
        // args[0] - input Visio file path (e.g., "input.vsdx")
        // args[1] - output HTML file path (e.g., "output.html")
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramHtmlExport <inputVisioPath> <outputHtmlPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML export options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Do not export hidden pages.
                ExportHiddenPage = false,
                // Assign the custom stream provider to embed resources as base64.
                StreamProvider = new Base64StreamProvider()
            };

            // Save as a single HTML file with embedded resources.
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram exported to HTML with embedded resources: {outputPath}");
        }
    }
}