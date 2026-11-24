using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram.
            Diagram sourceDiagram = new Diagram(inputPath);

            // Determine the directory for output files.
            string outputDir = Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();

            // Iterate over each page in the source diagram.
            for (int i = 0; i < sourceDiagram.Pages.Count; i++)
            {
                // Retrieve the current page.
                Page srcPage = sourceDiagram.Pages[i];

                // Create a new empty diagram for the single page export.
                Diagram singlePageDiagram = new Diagram();

                // Copy all masters from the source diagram to the new diagram.
                foreach (Master master in sourceDiagram.Masters)
                {
                    // Add each master to the new diagram.
                    singlePageDiagram.Masters.Add(master);
                }

                // Create a new page and copy the contents of the source page.
                Page newPage = new Page();
                newPage.Copy(srcPage);

                // ----- Resize the page -----
                // Example: set width to 10 inches and height to 7.5 inches.
                // Adjust these values as needed for your specific resizing requirements.
                newPage.PageSheet.PageProps.PageWidth.Value = 10.0;
                newPage.PageSheet.PageProps.PageHeight.Value = 7.5;

                // Add the resized page to the new diagram.
                singlePageDiagram.Pages.Add(newPage);

                // Build the output file name using the page index.
                string outputPath = Path.Combine(outputDir, $"Page_{i}.vsdx");

                // Save the single‑page diagram as VSDX.
                singlePageDiagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Exported page {i} to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}