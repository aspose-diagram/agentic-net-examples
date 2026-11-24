using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a list to hold image streams for each page
                List<MemoryStream> imageStreams = new List<MemoryStream>();

                // Iterate over each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Create a new memory stream for the current page image
                    MemoryStream pageStream = new MemoryStream();

                    // Configure image save options for PNG format, exporting a single page
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export only the current page
                        PageIndex = i,
                        PageCount = 1
                    };

                    // Save the current page to the memory stream
                    diagram.Save(pageStream, saveOptions);

                    // Reset stream position for potential downstream use
                    pageStream.Position = 0;

                    // Add the stream to the collection
                    imageStreams.Add(pageStream);
                }

                // Validate that the number of generated streams matches the page count
                if (imageStreams.Count != diagram.Pages.Count)
                {
                    // Mismatch detected – report and throw an exception
                    Console.Error.WriteLine($"Mismatch: Generated {imageStreams.Count} image streams but diagram contains {diagram.Pages.Count} pages.");
                    throw new Exception("Image stream count does not match page count.");
                }
                else
                {
                    // Success – report the validation result
                    Console.WriteLine($"Validation successful: {imageStreams.Count} image streams generated for {diagram.Pages.Count} pages.");
                }

                // Optional: Dispose all memory streams (handled by using block exit if needed)
                foreach (var ms in imageStreams)
                {
                    ms.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}