using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define output file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page in the diagram
            Page page = diagram.Pages[0];

            // Locate the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Compare the deletion flag using the BOOL enum
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("No shape found in the diagram.");
            }

            // Assign a custom script to the EventXFMod cell (shape data changed event)
            // This formula invokes a VBA macro named RecalcDependentValues
            targetShape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"RecalcDependentValues\")";

            // Save the modified diagram using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}