using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace AsposeDiagramStreamProviderTest
{
    // Custom IStreamProvider implementation that records the default path of each resource
    public class TestStreamProvider : IStreamProvider
    {
        // List to store the default path (file name) of each received resource
        public List<string> ReceivedPaths { get; } = new List<string>();

        // Called by Aspose.Diagram before writing a resource (image, shape, etc.)
        public void InitStream(StreamProviderOptions options)
        {
            // Record the default path which contains the file name and extension
            ReceivedPaths.Add(options.DefaultPath);

            // Provide a dummy memory stream for the resource data
            options.Stream = new MemoryStream();
        }

        // Called after the resource has been written
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the dummy stream if it was created
            options.Stream?.Dispose();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Use the first page (created by default)
                Page page = diagram.Pages[0];

                // Add a simple rectangle shape using a built‑in master name
                // The AddShape method returns the shape ID (long)
                long rectShapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape instance to ensure it was created successfully
                Shape rectShape = page.Shapes.GetShape(rectShapeId);
                if (rectShape == null)
                    throw new Exception("Failed to create rectangle shape.");

                // Add an image shape using an empty memory stream as placeholder image data
                using (MemoryStream dummyImage = new MemoryStream())
                {
                    long imageShapeId = page.AddShape(5.0, 5.0, 2.0, 2.0, dummyImage);
                    Shape imageShape = page.Shapes.GetShape(imageShapeId);
                    if (imageShape == null)
                        throw new Exception("Failed to create image shape.");
                }

                // Prepare HTML export options and assign the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                TestStreamProvider provider = new TestStreamProvider();
                htmlOptions.StreamProvider = provider;

                // Export the diagram to HTML (the file path is irrelevant for the test)
                diagram.Save("test_output.html", htmlOptions);

                // Verify that the provider received both image and shape resource types
                // Image resources typically have a .png extension, shape resources a .svg extension
                bool hasImage = provider.ReceivedPaths.Exists(p => p.EndsWith(".png", StringComparison.OrdinalIgnoreCase));
                bool hasShape = provider.ReceivedPaths.Exists(p => p.EndsWith(".svg", StringComparison.OrdinalIgnoreCase));

                if (!hasImage || !hasShape)
                    throw new Exception($"IStreamProvider did not receive expected resource types. Image: {hasImage}, Shape: {hasShape}");

                // Output result to console for visual confirmation
                Console.WriteLine("IStreamProvider received the following resource paths:");
                foreach (string path in provider.ReceivedPaths)
                {
                    Console.WriteLine($"- {path}");
                }

                Console.WriteLine("Test completed successfully.");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream and exit
                Console.Error.WriteLine($"Error: {ex.Message}");
                return;
            }
        }
    }
}