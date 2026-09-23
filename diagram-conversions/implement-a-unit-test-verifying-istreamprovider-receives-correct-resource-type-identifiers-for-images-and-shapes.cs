using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace IStreamProviderTest
{
    // Custom stream provider to capture resource type identifiers during HTML export
    public class TestStreamProvider : IStreamProvider
    {
        // List to store the resource type identifiers received in InitStream
        public List<string> ReceivedResourceTypes { get; } = new List<string>();

        // Called by Aspose.Diagram when a resource stream is initialized
        public void InitStream(StreamProviderOptions options)
        {
            // Guard against null options
            if (options == null) return;

            // Determine resource type based on the default path (image files usually have an extension)
            string type = "Shape"; // default assumption
            if (!string.IsNullOrEmpty(options.DefaultPath) &&
                options.DefaultPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                type = "Image";
            }

            // Record the inferred type
            ReceivedResourceTypes.Add(type);

            // Provide a dummy stream to satisfy the export process
            options.Stream = new MemoryStream();
        }

        // Called by Aspose.Diagram when a resource stream is closed
        public void CloseStream(StreamProviderOptions options)
        {
            // Guard against null options
            if (options?.Stream == null) return;

            // Dispose the dummy stream
            options.Stream.Dispose();
            options.Stream = null;
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {
                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Access the first (and only) page
                Page page = diagram.Pages[0];

                // Add a simple rectangle shape
                long rectShapeId = page.AddShape(1.0, 1.0, 2.0, 1.0, "Rectangle", false);
                Shape rectShape = page.Shapes.GetShape(rectShapeId);
                rectShape.Text.Value.Add(new Txt("Rectangle Shape"));

                // Add an image shape using a dummy PNG byte array
                byte[] dummyPng = new byte[]
                {
                    0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A,
                    0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52,
                    0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01,
                    0x08, 0x02, 0x00, 0x00, 0x00, 0x90, 0x77, 0x53,
                    0xDE, 0x00, 0x00, 0x00, 0x0A, 0x49, 0x44, 0x41,
                    0x54, 0x08, 0xD7, 0x63, 0x60, 0x00, 0x00, 0x00,
                    0x02, 0x00, 0x01, 0xE2, 0x21, 0xBC, 0x33, 0x00,
                    0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE,
                    0x42, 0x60, 0x82
                };
                using (MemoryStream imgStream = new MemoryStream(dummyPng))
                {
                    // AddShape overload that accepts a stream creates a foreign (image) shape
                    long imgShapeId = page.AddShape(4.0, 1.0, 2.0, 2.0, imgStream);
                    // No additional configuration needed for the image shape
                }

                // Prepare HTML export options with the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                TestStreamProvider provider = new TestStreamProvider();
                htmlOptions.StreamProvider = provider;

                // Export the diagram to HTML (output path is irrelevant for the test)
                diagram.Save("test_output.html", htmlOptions);

                // Verify that the stream provider received both image and shape resource types
                bool hasImage = false;
                bool hasShape = false;
                foreach (string type in provider.ReceivedResourceTypes)
                {
                    Console.WriteLine($"Received resource type: {type}");
                    if (type.Equals("Image", StringComparison.OrdinalIgnoreCase))
                        hasImage = true;
                    if (type.Equals("Shape", StringComparison.OrdinalIgnoreCase))
                        hasShape = true;
                }

                if (!hasImage)
                    throw new Exception("IStreamProvider did not receive an Image resource type.");

                if (!hasShape)
                    throw new Exception("IStreamProvider did not receive a Shape resource type.");

                Console.WriteLine("IStreamProvider correctly received both Image and Shape resource types.");
            }
            catch (Exception ex)
            {
                // Write any errors to the error stream
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}