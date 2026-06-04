using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace AsposeDiagramIStreamProviderTest
{
    // Custom IStreamProvider that records the resource names requested during HTML export
    public class RecordingStreamProvider : IStreamProvider
    {
        // List to store the DefaultPath values received in InitStream calls
        public List<string> RecordedPaths { get; } = new List<string>();

        // Called by Aspose.Diagram when a resource stream is needed
        public void InitStream(StreamProviderOptions options)
        {
            // Record the resource identifier (DefaultPath) for verification
            if (!string.IsNullOrEmpty(options.DefaultPath))
            {
                RecordedPaths.Add(options.DefaultPath);
            }

            // Provide a writable stream (in‑memory) for the resource
            options.Stream = new MemoryStream();
        }

        // Called after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream we created in InitStream
            options.Stream?.Dispose();
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Create a simple diagram with one rectangle shape
                var diagram = new Diagram();
                // Add a rectangle shape on the first page (page index 0)
                long shapeId = diagram.AddShape(1.0, 1.0, "Rectangle", 0);
                // Retrieve the shape to set some text (optional, just to ensure content)
                var page = diagram.Pages[0];
                var shape = page.Shapes.GetShape(shapeId);
                shape.Text.Value.Add(new Txt("Test Shape"));

                // Prepare HTML export options and attach the custom stream provider
                var htmlOptions = new HTMLSaveOptions();
                var provider = new RecordingStreamProvider();
                htmlOptions.StreamProvider = provider;

                // Export the diagram to HTML using a memory stream (no file I/O)
                using (var outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, htmlOptions);
                }

                // Verify that the stream provider was invoked and received resource names
                if (provider.RecordedPaths.Count == 0)
                {
                    throw new Exception("IStreamProvider was not invoked during HTML export.");
                }

                // Example verification: each recorded path should be non‑empty
                foreach (var path in provider.RecordedPaths)
                {
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        throw new Exception("IStreamProvider received an empty resource name.");
                    }
                    // Optional: you can add more specific checks here, e.g., file extensions
                    // Console.WriteLine($"Resource requested: {path}");
                }

                Console.WriteLine("IStreamProvider test passed. Recorded resource names:");
                foreach (var path in provider.RecordedPaths)
                {
                    Console.WriteLine(path);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}