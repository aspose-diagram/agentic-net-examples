using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Expected style guidelines (hard‑coded for demonstration)
    private const string ExpectedLineColor = "#000000";   // Black
    private const string ExpectedFillColor = "#FFFFFF";   // White
    private const string ExpectedFontName  = "Arial";

    static void Main(string[] args)
    {
        // -----------------------------------------------------------------
        // Validate command‑line arguments
        // -----------------------------------------------------------------
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // -----------------------------------------------------------------
        // Guard: ensure the input file exists
        // -----------------------------------------------------------------
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found – {visioPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Load the Visio diagram
            // -----------------------------------------------------------------
            Diagram diagram = new Diagram(visioPath);

            // -----------------------------------------------------------------
            // Iterate through all masters in the document
            // -----------------------------------------------------------------
            for (int m = 0; m < diagram.Masters.Count; m++)
            {
                Master master = diagram.Masters[m];

                // -----------------------------------------------------------------
                // Iterate through each shape that belongs to the current master
                // -----------------------------------------------------------------
                for (int s = 0; s < master.Shapes.Count; s++)
                {
                    Shape shape = master.Shapes[s];

                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // -----------------------------------------------------------------
                    // Validate line color (inherited from master/style)
                    // -----------------------------------------------------------------
                    string lineColor = shape.InheritLine.LineColor.Value;
                    if (!string.Equals(lineColor, ExpectedLineColor, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Error.WriteLine(
                            $"Master \"{master.Name}\" – Shape ID {shape.ID}: Line color \"{lineColor}\" does not match expected \"{ExpectedLineColor}\".");
                    }

                    // -----------------------------------------------------------------
                    // Validate fill foreground color (inherited)
                    // -----------------------------------------------------------------
                    string fillColor = shape.InheritFill.FillForegnd.Value;
                    if (!string.Equals(fillColor, ExpectedFillColor, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Error.WriteLine(
                            $"Master \"{master.Name}\" – Shape ID {shape.ID}: Fill color \"{fillColor}\" does not match expected \"{ExpectedFillColor}\".");
                    }

                    // -----------------------------------------------------------------
                    // Validate font name of the first character (inherited)
                    // -----------------------------------------------------------------
                    // InheritChars may contain zero characters; guard against that.
                    try
                    {
                        // Get the first inherited character formatting
                        var inheritedChar = shape.InheritChars.GetChar(0);
                        string fontName = inheritedChar.FontName.Value;

                        if (!string.Equals(fontName, ExpectedFontName, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Error.WriteLine(
                                $"Master \"{master.Name}\" – Shape ID {shape.ID}: Font \"{fontName}\" does not match expected \"{ExpectedFontName}\".");
                        }
                    }
                    catch (Exception ex)
                    {
                        // If GetChar(0) fails (e.g., no characters), report and continue
                        Console.Error.WriteLine(
                            $"Master \"{master.Name}\" – Shape ID {shape.ID}: Unable to retrieve inherited font information. {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Master shape style validation completed.");
        }
        catch (Exception ex)
        {
            // -----------------------------------------------------------------
            // Catch any Aspose.Diagram related exceptions and report them
            // -----------------------------------------------------------------
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}