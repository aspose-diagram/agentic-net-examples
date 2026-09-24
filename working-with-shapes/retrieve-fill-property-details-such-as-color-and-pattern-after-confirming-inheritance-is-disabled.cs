using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine diagram file path (use first argument or a default placeholder)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the diagram file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram (inheritance handling is performed internally)
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True) continue;

                    // Determine if fill inheritance is disabled by comparing local fill values
                    bool isFillForegndInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                    bool isFillPatternInherited = shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;

                    // If either fill foreground color or pattern is not inherited, treat inheritance as disabled
                    if (!isFillForegndInherited || !isFillPatternInherited)
                    {
                        // Retrieve fill foreground color (hex string)
                        string foreColor = shape.Fill.FillForegnd.Value;

                        // Retrieve fill background color (hex string)
                        string backColor = shape.Fill.FillBkgnd.Value;

                        // Retrieve fill pattern index (integer)
                        int pattern = (int)shape.Fill.FillPattern.Value;

                        // Output shape identification and its fill details
                        Console.WriteLine($"Page {page.ID}, Shape {shape.ID} ('{shape.Name}'):");
                        Console.WriteLine($"  Fill Foreground: {foreColor}");
                        Console.WriteLine($"  Fill Background: {backColor}");
                        Console.WriteLine($"  Fill Pattern: {pattern}");
                        Console.WriteLine();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}