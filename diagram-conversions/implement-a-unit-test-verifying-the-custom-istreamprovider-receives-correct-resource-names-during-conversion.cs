using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramHtmlExportTest
{
    // Custom stream provider that records the resource names requested by the HTML exporter.
    public class RecordingStreamProvider : IStreamProvider
    {
        private readonly List<string> _resourceNames;

        public RecordingStreamProvider(List<string> resourceNames)
        {
            _resourceNames = resourceNames ?? throw new ArgumentNullException(nameof(resourceNames));
        }

        // Called by Aspose.Diagram when a resource (e.g., image, CSS) needs a stream.
        public void InitStream(StreamProviderOptions options)
        {
            // Record the default path (resource name) supplied by the exporter.
            _resourceNames.Add(options.DefaultPath);

            // Provide a writable stream; for this test we use a MemoryStream.
            options.Stream = new MemoryStream();
        }

        // Called after the exporter finishes writing to the stream.
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream we created in InitStream.
            options.Stream?.Dispose();
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Prepare a list to capture resource names.
                List<string> capturedResources = new List<string>();

                // Create a simple diagram with one rectangle shape.
                Diagram diagram = new Diagram();
                // Add a rectangle master shape to the diagram (master name "Rectangle" exists in the default stencil).
                long shapeId = diagram.AddShape(2.0, 2.0, 4.0, 2.0, "Rectangle", 0);
                // Retrieve the shape to set some text (optional, ensures content is present).
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                shape.Text.Value.Add(new Txt("Sample shape"));

                // Configure HTML export options and assign the custom stream provider.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new RecordingStreamProvider(capturedResources);

                // Define output paths.
                string outputHtml = "test_output.html";

                // Perform the HTML export. The custom stream provider will be invoked for each resource.
                diagram.Save(outputHtml, htmlOptions);

                // Verify that the stream provider received at least one resource name.
                if (capturedResources.Count == 0)
                {
                    throw new Exception("IStreamProvider was not invoked; no resource names were captured.");
                }

                // Example verification: ensure that at least one captured resource ends with ".png"
                bool hasPng = false;
                foreach (string name in capturedResources)
                {
                    Console.WriteLine($"Captured resource: {name}");
                    if (name.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        hasPng = true;
                    }
                }

                if (!hasPng)
                {
                    throw new Exception("Expected at least one PNG resource, but none were captured.");
                }

                Console.WriteLine("IStreamProvider test passed. Captured resource count: " + capturedResources.Count);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}