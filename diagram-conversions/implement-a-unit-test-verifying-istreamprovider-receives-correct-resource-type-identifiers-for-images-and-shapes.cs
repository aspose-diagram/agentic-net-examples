using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace AsposeDiagramStreamProviderTest
{
    // Custom IStreamProvider implementation to capture resource identifiers
    public class TestStreamProvider : IStreamProvider
    {
        // Stores the DefaultPath values received during InitStream calls
        public List<string> ReceivedPaths { get; } = new List<string>();

        // Called by Aspose when a resource stream is initialized
        public void InitStream(StreamProviderOptions options)
        {
            // Record the default path which identifies the resource (e.g., image or shape)
            if (!string.IsNullOrEmpty(options.DefaultPath))
            {
                ReceivedPaths.Add(options.DefaultPath);
            }

            // Provide a writable stream for the resource (using a MemoryStream here)
            options.Stream = new MemoryStream();
        }

        // Called by Aspose when the resource stream is closed
        public void CloseStream(StreamProviderOptions options)
        {
            // Dispose the stream if it was created
            options.Stream?.Dispose();
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

                // Get the first page (created by default)
                Page page = diagram.Pages[0];

                // Add a simple rectangle shape
                long rectShapeId = page.AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle");
                Shape rectShape = page.Shapes.GetShape(rectShapeId);
                rectShape.NameU = "TestRectangle";

                // Add an image shape using a dummy PNG stream (1x1 pixel)
                byte[] pngBytes = new byte[]
                {
                    0x89,0x50,0x4E,0x47,0x0D,0x0A,0x1A,0x0A,
                    0x00,0x00,0x00,0x0D,0x49,0x48,0x44,0x52,
                    0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,
                    0x08,0x06,0x00,0x00,0x00,0x1F,0x15,0xC4,
                    0x89,0x00,0x00,0x00,0x0A,0x49,0x44,0x41,
                    0x54,0x78,0x9C,0x63,0x60,0x00,0x00,0x00,
                    0x02,0x00,0x01,0xE2,0x21,0xBC,0x33,0x00,
                    0x00,0x00,0x00,0x49,0x45,0x4E,0x44,0xAE,
                    0x42,0x60,0x82
                };
                using (MemoryStream imageStream = new MemoryStream(pngBytes))
                {
                    long imgShapeId = page.AddShape(4.0, 2.0, 1.0, 1.0, imageStream);
                    Shape imgShape = page.Shapes.GetShape(imgShapeId);
                    imgShape.NameU = "TestImage";
                }

                // Prepare HTML export options with the custom stream provider
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                TestStreamProvider provider = new TestStreamProvider();
                htmlOptions.StreamProvider = provider;

                // Export the diagram to HTML (output path can be any temporary location)
                string outputPath = "test_output.html";
                diagram.Save(outputPath, htmlOptions);

                // Verify that the stream provider received at least one image and one shape resource
                bool hasImageResource = false;
                bool hasShapeResource = false;

                foreach (string path in provider.ReceivedPaths)
                {
                    // Simple heuristic: image resources often contain file extensions like .png, .jpg, etc.
                    if (path.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        path.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        path.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        path.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
                        path.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                    {
                        hasImageResource = true;
                    }
                    else
                    {
                        // Assume non‑image resources correspond to shape HTML fragments
                        hasShapeResource = true;
                    }
                }

                // Manual assertions using exceptions as per project constraints
                if (!hasImageResource)
                {
                    throw new Exception("IStreamProvider did not receive any image resource identifiers during HTML export.");
                }

                if (!hasShapeResource)
                {
                    throw new Exception("IStreamProvider did not receive any shape resource identifiers during HTML export.");
                }

                Console.WriteLine("IStreamProvider correctly received image and shape resource identifiers.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }
}