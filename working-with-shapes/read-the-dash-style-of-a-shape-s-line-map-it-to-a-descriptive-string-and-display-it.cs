using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (adjust as needed)
        string inputPath = "input.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a line definition (Line may be null for some shapes)
                    if (shape.Line != null)
                    {
                        // Retrieve the line dash pattern enum value from the shape's line cell
                        LinePatternValue pattern = shape.Line.LinePattern.Value;

                        // Map the enum to a human‑readable description
                        string description = GetLinePatternDescription(pattern);

                        // Display the shape ID and its line dash style description
                        Console.WriteLine($"Shape ID {shape.ID} – Line dash style: {description}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Converts a LinePatternValue enum to a descriptive string.
    /// </summary>
    private static string GetLinePatternDescription(LinePatternValue pattern)
    {
        // Map known enum values to friendly names; unknown values fall back to "Unknown"
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
            case LinePatternValue.LongDash:
                return "Long Dash";
            // The following members may not exist in older Aspose.Diagram versions;
            // they are omitted to avoid compilation errors.
            default:
                return "Unknown";
        }
    }
}