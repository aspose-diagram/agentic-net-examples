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

        // Guard: ensure the output folder exists (create if necessary).
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: Unable to create output folder '{outputFolder}'. {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate over each page to generate a thumbnail.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Prepare image save options for PNG thumbnail.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the current page.
                    PageIndex = i,
                    PageCount = 1,
                    // Scale down to create a thumbnail (e.g., 20% of original size).
                    Scale = 0.2f
                };

                // Build the output file name: Page_1.png, Page_2.png, etc.
                string thumbnailPath = Path.Combine(outputFolder, $"Page_{i + 1}.png");

                // Save the thumbnail image.
                diagram.Save(thumbnailPath, saveOptions);
                Console.WriteLine($"Thumbnail saved: {thumbnailPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}