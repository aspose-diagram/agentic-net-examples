using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramImageValidator
{
    // Path to the source Visio diagram
    private const string DiagramPath = @"C:\Diagrams\sample.vsdx";

    static void Main()
    {
        try
        {

            // Load the diagram using Aspose.Diagram
            Diagram diagram = new Diagram(DiagramPath);

            // Get the total number of pages in the source diagram
            int totalPages = diagram.Pages.Count;

            // List to hold image streams generated for each page
            List<MemoryStream> imageStreams = new List<MemoryStream>();

            // Prepare save options for image export (PNG format)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                // Ensure only one page is rendered per save operation
                PageCount = 1
            };

            // Generate an image stream for each page
            for (int i = 0; i < totalPages; i++)
            {
                // Set the page index to render (zero‑based)
                saveOptions.PageIndex = i;

                // Create a memory stream to hold the image data
                MemoryStream ms = new MemoryStream();

                // Save the current page to the memory stream
                diagram.Save(ms, saveOptions);

                // Reset stream position for later consumption
                ms.Position = 0;

                // Store the stream in the collection
                imageStreams.Add(ms);
            }

            // Validate that the number of generated streams matches the page count
            if (imageStreams.Count != totalPages)
            {
                throw new InvalidOperationException(
                    $"Mismatch: diagram has {totalPages} pages but {imageStreams.Count} image streams were generated.");
            }

            Console.WriteLine($"Validation successful: {imageStreams.Count} image streams generated for {totalPages} pages.");

            // (Optional) Dispose streams when done
            foreach (var stream in imageStreams)
            {
                stream.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
