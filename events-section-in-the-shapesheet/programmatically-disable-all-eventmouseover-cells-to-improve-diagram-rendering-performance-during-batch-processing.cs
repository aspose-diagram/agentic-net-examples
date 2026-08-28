using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // required for SaveFileFormat

class Program
{
    static void Main(string[] args)
    {
        // Resolve input and output file paths (allow command‑line overrides)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source diagram file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over every page and every shape within each page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape has an Event section before accessing its cells
                    if (shape.Event != null)
                    {
                        // Clear formulas for all supported event cells to disable mouse‑over actions
                        shape.Event.EventXFMod.Ufe.F = "";      // Mouse‑over formula (if present)
                        shape.Event.EventDblClick.Ufe.F = "";   // Double‑click event
                        shape.Event.EventDrop.Ufe.F = "";       // Drop event
                        shape.Event.EventMultiDrop.Ufe.F = "";  // Multi‑drop event
                        shape.Event.TheText.Ufe.F = "";         // Text‑related event
                        shape.Event.TheData.Ufe.F = "";         // Data‑related event
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine("An error occurred:");
            Console.Error.WriteLine(ex.Message);
            throw;
        }
    }
}