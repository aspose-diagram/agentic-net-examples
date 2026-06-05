using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExport
{
    // Custom stream provider to embed JavaScript into the exported HTML package
    public class CustomStreamProvider : IStreamProvider
    {
        // Called by Aspose when a resource stream is required (e.g., a .js file)
        public void InitStream(StreamProviderOptions options)
        {
            // Provide a simple JavaScript file named "script.js"
            // The options.DefaultPath contains the requested resource path
            if (options.DefaultPath != null && options.DefaultPath.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
            {
                string jsContent = "function MyJSFunction(){ alert('Shape double-clicked'); }";
                MemoryStream ms = new MemoryStream();
                StreamWriter writer = new StreamWriter(ms);
                writer.Write(jsContent);
                writer.Flush();
                ms.Position = 0;
                options.Stream = ms;
            }
            else
            {
                // For any other resource, provide an empty stream
                options.Stream = new MemoryStream();
            }
        }

        // Called after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Use the first page (default page is always present)
                Page page = diagram.Pages[0];

                // Add a rectangle shape at position (2,2) using the built‑in master name "Rectangle"
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object
                Shape shape = page.Shapes.GetShape(shapeId);

                // Assign a double‑click event that calls a JavaScript function defined in the HTML export
                shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyJSFunction\")";

                // Prepare HTML save options and attach the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new CustomStreamProvider()
                };

                // Export the diagram to an HTML file; the JavaScript will be embedded via the stream provider
                diagram.Save("output.html", htmlOptions);

                Console.WriteLine("HTML export completed. Open 'output.html' to test shape interaction.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}