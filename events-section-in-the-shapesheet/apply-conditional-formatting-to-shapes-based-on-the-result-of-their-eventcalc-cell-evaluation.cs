using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the diagram inside a try/catch to capture any loading errors
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages in the diagram
        foreach (Page page in diagram.Pages)
        {
            // Iterate through all shapes on the current page
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted
                if (shape.Del == BOOL.True)
                    continue;

                // Access a valid event cell (EventXFMod) – EventCalc does not exist
                // The .Ufe.F property contains the formula or its evaluated result as a string
                string eventResult = shape.Event.EventXFMod.Ufe.F;

                // Apply conditional formatting based on the event cell result
                if (!string.IsNullOrEmpty(eventResult) &&
                    (eventResult.Equals("1", StringComparison.OrdinalIgnoreCase) ||
                     eventResult.Equals("TRUE", StringComparison.OrdinalIgnoreCase)))
                {
                    // Set fill foreground color to red when condition is met
                    shape.Fill.FillForegnd.Value = "#FF0000";
                }
                else
                {
                    // Set fill foreground color to green otherwise
                    shape.Fill.FillForegnd.Value = "#00FF00";
                }
            }
        }

        // Output Visio file path
        string outputPath = "output.vsdx";
        try
        {
            // Save the modified diagram using a valid SaveFileFormat enum value
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}