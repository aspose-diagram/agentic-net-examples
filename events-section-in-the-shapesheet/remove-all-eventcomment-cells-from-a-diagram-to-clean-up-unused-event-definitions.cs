using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (command‑line arguments or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_cleaned.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // The EventComment cell is not exposed in Aspose.Diagram.
                        // As a practical substitute, clear the generic event cells
                        // (EventDrop and EventXFMod) which are the closest equivalents.
                        if (shape.Event != null)
                        {
                            // Clear EventDrop formula
                            shape.Event.EventDrop.Ufe.F = "";
                            // Clear EventXFMod formula
                            shape.Event.EventXFMod.Ufe.F = "";
                        }
                    }
                }

                // Save the cleaned diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved without EventComment equivalents to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}