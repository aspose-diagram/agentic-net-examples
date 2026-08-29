using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure the required arguments (input file and output folder) are provided
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputFolder>");
            return;
        }

        // Assign the input Visio file path
        string inputPath = args[0];
        // Guard: verify the Visio file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign the output folder path where thumbnails will be stored
        string outputFolder = args[1];
        // Guard: create the output folder if it does not already exist
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Load the Visio diagram inside a try/catch to capture any loading errors
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate over each page in the diagram, keeping a zero‑based index for export
        int pageIndex = 0;
        foreach (Page page in diagram.Pages)
        {
            // Build a safe file name using the page name (or fallback) and its ID
            string safeName = string.IsNullOrWhiteSpace(page.Name) ? $"Page_{page.ID}" : page.Name;
            // Replace any characters that are invalid in file names with an underscore
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                safeName = safeName.Replace(c, '_');
            }
            // Combine the output folder with the generated file name and PNG extension
            string outputPath = Path.Combine(outputFolder, $"{safeName}_{page.ID}.png");

            // Export the current page to a PNG thumbnail inside its own try/catch
            try
            {
                // Create image save options targeting PNG format
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                // Set the page index to the current page (zero‑based)
                options.PageIndex = pageIndex;
                // Export only this single page
                options.PageCount = 1;
                // Optionally set a lower resolution for a smaller thumbnail (e.g., 150 DPI)
                options.Resolution = 150;
                // Save the thumbnail image to the designated path
                diagram.Save(outputPath, options);
                Console.WriteLine($"Thumbnail saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving thumbnail for page {page.ID}: {ex.Message}");
            }

            // Increment the page index for the next iteration
            pageIndex++;
        }
    }
}