using System;
using System.Collections.Generic;
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
            Diagram diagram = new Diagram("input.vsdx");

            // Total number of pages in the diagram
            int totalPages = diagram.Pages.Count;

            // Collection to store image streams for each page
            List<MemoryStream> imageStreams = new List<MemoryStream>();

            // Save each page to a separate memory stream
            for (int i = 0; i < totalPages; i++)
            {
                // Create a memory stream for the current page image
                MemoryStream ms = new MemoryStream();

                // Configure save options to render only the current page
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = i;   // zero‑based index of the page to render
                options.PageCount = 1;   // render a single page

                // Save the page into the memory stream
                diagram.Save(ms, options);

                // Reset stream position for potential further use
                ms.Position = 0;

                imageStreams.Add(ms);
            }

            // Validate that the number of generated streams matches the number of pages
            bool isValid = imageStreams.Count == totalPages;

            // Output validation result
            Console.WriteLine($"Diagram pages: {totalPages}");
            Console.WriteLine($"Generated image streams: {imageStreams.Count}");
            Console.WriteLine($"Validation passed: {isValid}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
