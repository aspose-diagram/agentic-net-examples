using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input file path and verify existence
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the verified path
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Build a conditional formula that calls different actions based on shape type
                    // Shape.Type = 1 → regular shape, Shape.Type = 2 → connector, else default
                    string formula = "IF(Shape.Type=1, CALLTHIS(\"RectangleAction\"), " +
                                     "IF(Shape.Type=2, CALLTHIS(\"ConnectorAction\"), " +
                                     "CALLTHIS(\"DefaultAction\")))";

                    // Assign the formula to a valid event cell (EventXFMod) using the Ufe.F property
                    shape.Event.EventXFMod.Ufe.F = formula;
                }
            }

            // Save the modified diagram to the output file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}