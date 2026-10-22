using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line argument for source diagram path
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string sourcePath = args[0];

        // Guard: ensure the source file exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Error: File not found - {sourcePath}");
            return;
        }

        // List to hold generated image streams
        List<MemoryStream> imageStreams = new List<MemoryStream>();

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(sourcePath);

            // Get the total number of pages in the diagram
            int pageCount = diagram.Pages.Count;

            // Iterate over each page and export it to a PNG image stream
            for (int i = 0; i < pageCount; i++)
            {
                // Create a new memory stream for the current page image
                MemoryStream ms = new MemoryStream();

                // Configure image save options for PNG format and single-page export
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                options.PageIndex = i;   // Zero‑based page index
                options.PageCount = 1;   // Export only the current page

                // Save the current page to the memory stream
                diagram.Save(ms, options);

                // Reset stream position for potential downstream reading
                ms.Position = 0;

                // Store the stream in the collection
                imageStreams.Add(ms);
            }

            // Validate that the number of generated streams matches the page count
            if (imageStreams.Count != pageCount)
            {
                throw new Exception($"Mismatch: Generated {imageStreams.Count} image streams but diagram has {pageCount} pages.");
            }
            else
            {
                Console.WriteLine($"Success: Generated {imageStreams.Count} image streams matching the {pageCount} pages.");
            }

            // Dispose the diagram object to free resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
            return;
        }
        finally
        {
            // Ensure all memory streams are properly disposed
            foreach (var stream in imageStreams)
            {
                stream.Dispose();
            }
        }
    }
}