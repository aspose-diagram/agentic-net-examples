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

            // List to hold generated image streams
            List<MemoryStream> imageStreams = new List<MemoryStream>();

            // Configure image save options (PNG format)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.PageCount = 1; // render one page per stream

            // Generate an image stream for each page
            for (int i = 0; i < totalPages; i++)
            {
                saveOptions.PageIndex = i; // specify the page to render
                MemoryStream ms = new MemoryStream();
                diagram.Save(ms, saveOptions);
                ms.Position = 0; // reset stream position for later use
                imageStreams.Add(ms);
            }

            // Validate that the number of streams matches the number of pages
            if (imageStreams.Count == totalPages)
            {
                Console.WriteLine("Validation succeeded: stream count matches page count.");
            }
            else
            {
                Console.WriteLine($"Validation failed: {imageStreams.Count} streams vs {totalPages} pages.");
            }

            // Clean up streams
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
