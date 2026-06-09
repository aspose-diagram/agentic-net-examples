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

            // Load the source diagram
            var diagram = new Diagram("input.vsdx");

            // Options for rendering shapes to images (PNG used as example)
            var imageOptions = new ImageSaveOptions(SaveFileFormat.Png);

            const long maxFileSize = 2 * 1024 * 1024; // 2 MB in bytes

            // Validate each shape's rendered image size
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    using (var ms = new MemoryStream())
                    {
                        shape.ToImage(ms, imageOptions);          // Render shape to memory
                        if (ms.Length > maxFileSize)               // Check size
                        {
                            throw new InvalidOperationException(
                                $"Shape ID {shape.ID} on page {page.ID} exceeds the 2 MB limit (size: {ms.Length} bytes).");
                        }
                    }
                }
            }

            // All images are within the allowed size – export the diagram
            diagram.Save("output.pdf", SaveFileFormat.Pdf);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
