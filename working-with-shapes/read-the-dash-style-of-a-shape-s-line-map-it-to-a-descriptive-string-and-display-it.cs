using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram file
        string filePath = "input.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the line dash pattern enum value (LinePatternValue)
                    LinePatternValue pattern = shape.Line.LinePattern.Value;

                    // Convert the enum value to a human‑readable description
                    string description = GetLinePatternDescription(pattern);

                    // Output the shape ID, page name, and line dash style description
                    Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' has line dash style: {description}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to map LinePatternValue to a readable description
    static string GetLinePatternDescription(LinePatternValue pattern)
    {
        switch (pattern)
        {
            case LinePatternValue.Solid:
                return "Solid";
            case LinePatternValue.Dash:
                return "Dash";
            case LinePatternValue.Dot:
                return "Dot";
            case LinePatternValue.DashDot:
                return "DashDot";
            case LinePatternValue.DashDotDot:
                return "DashDotDot";
            case LinePatternValue.LongDash:
                return "LongDash";
            // The following patterns are not available in the current Aspose.Diagram version,
            // so they are omitted to avoid compilation errors.
            default:
                return "Unknown";
        }
    }
}