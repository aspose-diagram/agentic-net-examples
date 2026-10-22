using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Company style guidelines (adjust these values as needed)
            string expectedLineColor = "#FF0000";      // Expected line color (hex)
            string expectedFillColor = "#00FF00";      // Expected fill foreground color (hex)
            string expectedFontName = "Arial";         // Expected font name
            string expectedFontColor = "#000000";      // Expected font color (hex)
            double expectedFontSize = 0.12;            // Expected font size (in inches)

            bool anyViolation = false;

            // Iterate through all masters in the diagram
            for (int masterIndex = 0; masterIndex < diagram.Masters.Count; masterIndex++)
            {
                Master master = diagram.Masters[masterIndex];

                // Iterate through each shape defined in the master
                foreach (Shape shape in master.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // ----- Validate line color -----
                    string lineColor = shape.InheritLine?.LineColor?.Value;
                    if (!string.Equals(lineColor, expectedLineColor, StringComparison.OrdinalIgnoreCase))
                    {
                        anyViolation = true;
                        Console.WriteLine($"[Violation] Master '{master.Name}' Shape ID {shape.ID}: Line color '{lineColor}' does not match expected '{expectedLineColor}'.");
                    }

                    // ----- Validate fill foreground color -----
                    string fillColor = shape.InheritFill?.FillForegnd?.Value;
                    if (!string.Equals(fillColor, expectedFillColor, StringComparison.OrdinalIgnoreCase))
                    {
                        anyViolation = true;
                        Console.WriteLine($"[Violation] Master '{master.Name}' Shape ID {shape.ID}: Fill foreground color '{fillColor}' does not match expected '{expectedFillColor}'.");
                    }

                    // ----- Validate inherited font settings -----
                    // Retrieve the first inherited character formatting (index 0)
                    var inheritedChar = shape.InheritChars?.GetChar(0);
                    if (inheritedChar != null)
                    {
                        // Font name
                        string fontName = inheritedChar.FontName?.Value;
                        if (!string.Equals(fontName, expectedFontName, StringComparison.OrdinalIgnoreCase))
                        {
                            anyViolation = true;
                            Console.WriteLine($"[Violation] Master '{master.Name}' Shape ID {shape.ID}: Font name '{fontName}' does not match expected '{expectedFontName}'.");
                        }

                        // Font color
                        string fontColor = inheritedChar.Color?.Value;
                        if (!string.Equals(fontColor, expectedFontColor, StringComparison.OrdinalIgnoreCase))
                        {
                            anyViolation = true;
                            Console.WriteLine($"[Violation] Master '{master.Name}' Shape ID {shape.ID}: Font color '{fontColor}' does not match expected '{expectedFontColor}'.");
                        }

                        // Font size
                        double fontSize = inheritedChar.Size?.Value ?? 0;
                        if (Math.Abs(fontSize - expectedFontSize) > 0.001)
                        {
                            anyViolation = true;
                            Console.WriteLine($"[Violation] Master '{master.Name}' Shape ID {shape.ID}: Font size '{fontSize}' does not match expected '{expectedFontSize}'.");
                        }
                    }
                    else
                    {
                        // If no inherited character info is available, report it
                        anyViolation = true;
                        Console.WriteLine($"[Violation] Master '{master.Name}' Shape ID {shape.ID}: No inherited character formatting found.");
                    }
                }
            }

            if (!anyViolation)
            {
                Console.WriteLine("All master shapes comply with the company style guidelines.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or IO exceptions
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}