using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramExportValidator
{
    // Size limit: 2 megabytes
    private const long MaxImageSizeBytes = 2L * 1024 * 1024;

    static void Main()
    {
        try
        {

            // Load the source Visio diagram (replace with actual file path)
            var diagram = new Diagram("input.vsdx");

            // Prepare image save options (PNG used as example)
            var imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Optional: set quality or other options here if needed
            };

            // Validate each shape's exported image size
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    using (var ms = new MemoryStream())
                    {
                        // Export shape to memory stream using the same options that will be used for final export
                        shape.ToImage(ms, imgOptions);

                        // Check the generated image size
                        if (ms.Length > MaxImageSizeBytes)
                        {
                            throw new InvalidOperationException(
                                $"Shape ID {shape.ID} on page {page.ID} exceeds the 2 MB size limit (size: {ms.Length} bytes).");
                        }
                    }
                }
            }

            // All images are within the allowed size – proceed with diagram export
            // Example: export the whole diagram as a PNG image (first page)
            diagram.Save("output.png", SaveFileFormat.Png);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
