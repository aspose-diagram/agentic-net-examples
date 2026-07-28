using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the dash pattern enum value
                    LinePatternValue pattern = shape.Line.LinePattern.Value;

                    // Convert the enum to a readable description
                    string description = GetDashStyleDescription(pattern);

                    // Output the shape ID and its dash style
                    Console.WriteLine($"Shape ID {shape.ID} dash style: {description}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Maps LinePatternValue enum members to descriptive strings
    static string GetDashStyleDescription(LinePatternValue pattern)
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
            default:
                return "Unknown";
        }
    }
}
