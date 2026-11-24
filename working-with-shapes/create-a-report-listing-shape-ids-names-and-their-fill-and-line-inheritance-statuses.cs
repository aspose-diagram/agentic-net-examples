using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Header for the report
            Console.WriteLine("ShapeID\tNameU\tFillInherited\tLineInherited");

            // Iterate through all pages
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve shape identifier and name
                    long shapeId = shape.ID;
                    string shapeName = shape.NameU ?? string.Empty;

                    // Determine fill inheritance status
                    bool isFillInherited = false;
                    try
                    {
                        // Compare the foreground fill color with the inherited value
                        isFillInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                    }
                    catch
                    {
                        // If any property is unavailable, treat as not inherited
                        isFillInherited = false;
                    }

                    // Determine line inheritance status
                    bool isLineInherited = false;
                    try
                    {
                        // Compare the line color with the inherited line color
                        isLineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                    }
                    catch
                    {
                        // If any property is unavailable, treat as not inherited
                        isLineInherited = false;
                    }

                    // Output the report line for the current shape
                    Console.WriteLine($"{shapeId}\t{shapeName}\t{isFillInherited}\t{isLineInherited}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}