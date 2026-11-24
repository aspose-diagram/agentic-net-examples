using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect diagram file path and shape name (NameU)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeNameU>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];

        // Guard: check if the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Flag to indicate if the shape was found
            bool shapeFound = false;

            // Iterate through all pages and shapes to locate the target shape by NameU
            foreach (Page page in diagram.Pages)
            {
                // Iterate using index to avoid potential enumeration issues
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a valid NameU before comparing
                    if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        shapeFound = true;

                        // Compare the line color with the inherited line color to determine inheritance
                        // Matching values indicate the shape is using the inherited line formatting
                        bool isLineInherited = false;

                        // Guard against null Line or InheritLine (should not be null in normal cases)
                        if (shape.Line != null && shape.InheritLine != null)
                        {
                            // Compare line color values
                            if (shape.Line.LineColor != null && shape.InheritLine.LineColor != null)
                            {
                                isLineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                            }
                        }

                        // Output the result
                        Console.WriteLine($"Shape '{targetShapeName}' found on page '{page.Name}'.");
                        Console.WriteLine($"Line inheritance status: {(isLineInherited ? "Inherited" : "Custom")}");

                        // Optionally, display the actual line color values for debugging
                        string lineColor = shape.Line?.LineColor?.Value ?? "N/A";
                        string inheritLineColor = shape.InheritLine?.LineColor?.Value ?? "N/A";
                        Console.WriteLine($"Current Line Color: {lineColor}");
                        Console.WriteLine($"Inherited Line Color: {inheritLineColor}");

                        // Since the shape is found, we can exit the loops
                        break;
                    }
                }

                if (shapeFound)
                    break;
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Error: Shape with NameU '{targetShapeName}' not found in the diagram.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O exceptions and report them
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}