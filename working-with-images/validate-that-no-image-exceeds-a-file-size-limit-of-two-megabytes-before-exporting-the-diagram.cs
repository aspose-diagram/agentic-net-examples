using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from a file (lifecycle rule: load)
            var diagram = new Diagram("input.vsdx");

            // Prepare image save options (used for both validation and final export)
            var imageOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Example: set JPEG quality if needed (optional)
            // imageOptions.JpegQuality = 90;

            const long maxFileSizeBytes = 2 * 1024 * 1024; // 2 MB

            // Validate each shape's rendered image size
            foreach (var page in diagram.Pages)
            {
                foreach (var shape in page.Shapes)
                {
                    using (var ms = new MemoryStream())
                    {
                        // Render shape to a memory stream
                        shape.ToImage(ms, imageOptions);

                        // Check the size of the rendered image
                        if (ms.Length > maxFileSizeBytes)
                        {
                            throw new InvalidOperationException(
                                $"Shape ID {shape.ID} on page {page.ID} exceeds the 2 MB limit (size: {ms.Length} bytes).");
                        }
                    }
                }
            }

            // All images are within the size limit; export the diagram (lifecycle rule: save)
            diagram.Save("output.png", imageOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
