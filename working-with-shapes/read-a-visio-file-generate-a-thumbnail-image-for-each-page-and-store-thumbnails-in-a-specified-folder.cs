using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <OutputFolder>");
            return;
        }

        string visioPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        // Ensure the output directory exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: Could not create output folder '{outputFolder}'. {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate over each page in the diagram.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure image save options for a thumbnail.
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                    options.PageIndex = i;          // Export only the current page.
                    options.PageCount = 1;          // Single page per image.
                    options.Scale = 0.2f;           // Scale down to 20% (float literal).

                    // Build the output file name for the thumbnail.
                    string thumbnailPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                    // Save the page as a PNG thumbnail.
                    diagram.Save(thumbnailPath, options);
                    Console.WriteLine($"Thumbnail for page {i + 1} saved to '{thumbnailPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}