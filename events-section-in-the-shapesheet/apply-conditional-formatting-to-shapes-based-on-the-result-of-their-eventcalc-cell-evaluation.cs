using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // -----------------------------------------------------------------
                    // Retrieve the formula from an event cell.
                    // The original requirement mentions "EventCalc", which is not
                    // exposed in the Aspose.Diagram API. As a closest substitute we
                    // use the EventDrop cell (shape.Event.EventDrop.Ufe.F). This
                    // contains the raw formula string.
                    // -----------------------------------------------------------------
                    string eventFormula = shape.Event.EventDrop.Ufe.F ?? string.Empty;

                    // Simple condition: treat the shape as "true" when the formula
                    // contains the word TRUE (case‑insensitive). Real evaluation of
                    // Visio formulas is not supported by the library.
                    bool conditionMet = eventFormula.IndexOf("TRUE", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (conditionMet)
                    {
                        // Apply conditional formatting for the "true" case
                        // Set fill foreground color to red
                        shape.Fill.FillForegnd.Value = "#FF0000";

                        // Set line pattern to dashed
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;
                    }
                    else
                    {
                        // Apply default formatting for the "false" case
                        // Set fill foreground color to white
                        shape.Fill.FillForegnd.Value = "#FFFFFF";

                        // Set line pattern to solid
                        shape.Line.LinePattern.Value = LinePatternValue.Solid;
                    }
                }
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}