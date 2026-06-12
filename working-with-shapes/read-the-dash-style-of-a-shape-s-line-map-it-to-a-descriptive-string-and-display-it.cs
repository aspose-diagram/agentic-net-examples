using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the line pattern enum value
                    LinePatternValue pattern = shape.Line.LinePattern.Value;

                    // Convert the enum to a readable description
                    string description = GetLinePatternDescription(pattern);

                    // Display the shape ID and its line style
                    Console.WriteLine($"Shape ID {shape.ID}: {description}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Maps LinePatternValue enum members to human‑readable strings
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
                return "Dash‑Dot";
            case LinePatternValue.DashDotDot:
                return "Dash‑Dot‑Dot";
            default:
                return "Unknown";
        }
    }
}
