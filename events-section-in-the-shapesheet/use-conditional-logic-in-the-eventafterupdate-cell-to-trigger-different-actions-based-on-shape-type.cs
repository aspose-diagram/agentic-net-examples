using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Paths for input and output diagrams
        string inputPath = "input.vsdx";
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        Diagram diagram;
        try
        {
            // Load the diagram from file
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            // Report loading errors
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Determine the master name of the shape (if any)
                string masterName = shape.Master != null ? shape.Master.Name : string.Empty;

                // Build a Visio formula that calls different macros based on the shape type
                string formula;
                if (masterName.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                {
                    // Action for rectangle shapes
                    formula = "CALLTHIS(\"HandleRectangle\")";
                }
                else if (masterName.Equals("Dynamic connector", StringComparison.OrdinalIgnoreCase))
                {
                    // Action for connector shapes
                    formula = "CALLTHIS(\"HandleConnector\")";
                }
                else
                {
                    // Default action for other shapes
                    formula = "CALLTHIS(\"HandleOther\")";
                }

                // Assign the formula to a valid event cell (EventXFMod is the After Update event)
                shape.Event.EventXFMod.Ufe.F = formula;
            }
        }

        try
        {
            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report saving errors
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}