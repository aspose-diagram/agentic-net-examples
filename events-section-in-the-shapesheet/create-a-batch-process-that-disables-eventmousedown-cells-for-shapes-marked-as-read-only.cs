using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard: ensure input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify read‑only shapes by checking if Delete protection is enabled
                    if (shape.Protection.LockDelete.Value == BOOL.True)
                    {
                        // NOTE: The EventMouseDown cell does not exist in the Aspose.Diagram API.
                        // If needed, other event cells (e.g., EventDblClick) could be cleared here.
                        // Example (commented out):
                        // shape.Event.EventDblClick.Ufe.F = "";
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Processing complete. Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}