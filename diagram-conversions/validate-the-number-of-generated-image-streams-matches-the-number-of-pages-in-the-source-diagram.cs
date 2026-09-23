using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // List to hold generated image streams
            List<MemoryStream> imageStreams = new List<MemoryStream>();

            // Export each page to a separate image stream
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Configure image save options for a single page
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = i;   // zero‑based page index
                options.PageCount = 1;   // export only this page

                // Create a memory stream to hold the image
                MemoryStream ms = new MemoryStream();

                // Save the page to the memory stream
                diagram.Save(ms, options);

                // Reset stream position for potential further use
                ms.Position = 0;

                // Store the stream
                imageStreams.Add(ms);
            }

            // Validate that the number of generated streams matches the number of pages
            if (imageStreams.Count != diagram.Pages.Count)
            {
                throw new Exception($"Mismatch: {imageStreams.Count} image streams generated, but diagram has {diagram.Pages.Count} pages.");
            }
            else
            {
                Console.WriteLine($"Success: Generated {imageStreams.Count} image streams for {diagram.Pages.Count} pages.");
            }

            // Optional: clean up streams (if not needed later)
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
