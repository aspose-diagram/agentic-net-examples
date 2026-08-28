using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace AsposeDiagramStreamProviderTest
{
    // Custom IStreamProvider that records the resource names (DefaultPath) requested during HTML export
    public class RecordingStreamProvider : IStreamProvider
    {
        // List to store the resource names received
        public List<string> ReceivedResourceNames { get; } = new();

        // Called by Aspose.Diagram when a resource stream is needed
        public void InitStream(StreamProviderOptions options)
        {
            // DefaultPath is read‑only and contains the name of the resource (e.g., an image file name)
            if (!string.IsNullOrEmpty(options.DefaultPath))
            {
                ReceivedResourceNames.Add(options.DefaultPath);
            }

            // Provide a writable stream for the resource; using a MemoryStream as a placeholder
            options.Stream = new MemoryStream();
        }

        // Called after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the temporary stream if it was created
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Prepare output folder
                string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeDiagramHtmlTest");
                Directory.CreateDirectory(outputFolder);
                string htmlOutputPath = Path.Combine(outputFolder, "diagram.html");

                // Create a new diagram
                using Diagram diagram = new Diagram();

                // Add a simple rectangle shape to ensure at least one resource is generated
                // Using the built‑in "Rectangle" master on the first page (page index 0)
                long shapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);
                // Retrieve the shape to set some text (optional, just to have content)
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                shape.Text.Value.Add(new Txt("Test Shape"));

                // Set up HTML save options with the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    StreamProvider = new RecordingStreamProvider()
                };

                // Save the diagram as HTML
                diagram.Save(htmlOutputPath, htmlOptions);

                // Verify that the stream provider received at least one resource name
                var provider = (RecordingStreamProvider)htmlOptions.StreamProvider;
                if (provider.ReceivedResourceNames.Count == 0)
                {
                    throw new Exception("IStreamProvider did not receive any resource names during HTML export.");
                }

                // Output the captured resource names for inspection
                Console.WriteLine("IStreamProvider captured the following resource names:");
                foreach (string name in provider.ReceivedResourceNames)
                {
                    Console.WriteLine($"- {name}");
                }

                Console.WriteLine("Test completed successfully.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}