using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Prepare PNG save options for the current page
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export only the current page
                        PageIndex = i,
                        PageCount = 1
                    };

                    // NOTE: Aspose.Diagram does not provide an Interlaced property for PNG export.
                    // The interlaced (progressive) option cannot be enabled via the current API.
                    // The PNG will be saved with the default (non‑interlaced) settings.

                    // Build output file name
                    string outputPath = $"output_page_{i + 1}.png";

                    // Save the current page as PNG
                    diagram.Save(outputPath, pngOptions);
                    Console.WriteLine($"Page {i + 1} saved to {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}