using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name}");

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine if the shape is using inherited line formatting
                        // Compare the line color with the inherited line color as an indicator
                        bool isLineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;

                        if (isLineInherited)
                        {
                            // Retrieve inherited dash style (LinePattern) and thickness (LineWeight)
                            // LinePattern.Value returns a LinePatternValue enum; cast to int for display
                            int dashStyle = (int)shape.InheritLine.LinePattern.Value;

                            // LineWeight.Value returns a double representing thickness in inches
                            double thickness = shape.InheritLine.LineWeight.Value;

                            Console.WriteLine($"  Shape ID {shape.ID} inherits line formatting:");
                            Console.WriteLine($"    Dash Style (LinePattern): {dashStyle}");
                            Console.WriteLine($"    Thickness (LineWeight): {thickness} inches");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}