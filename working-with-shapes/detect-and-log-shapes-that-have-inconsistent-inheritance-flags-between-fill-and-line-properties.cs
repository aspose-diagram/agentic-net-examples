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

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if fill is inherited by comparing current fill foreground with inherited fill foreground
                    bool isFillInherited = false;
                    try
                    {
                        isFillInherited = string.Equals(
                            shape.Fill.FillForegnd.Value,
                            shape.InheritFill.FillForegnd.Value,
                            StringComparison.OrdinalIgnoreCase);
                    }
                    catch (Exception ex)
                    {
                        // If any property is unavailable, treat as not inherited and log the issue
                        Console.Error.WriteLine($"Warning: Unable to evaluate fill inheritance for shape ID {shape.ID} on page '{page.Name}'. {ex.Message}");
                    }

                    // Determine if line is inherited by comparing current line color with inherited line color
                    bool isLineInherited = false;
                    try
                    {
                        isLineInherited = string.Equals(
                            shape.Line.LineColor.Value,
                            shape.InheritLine.LineColor.Value,
                            StringComparison.OrdinalIgnoreCase);
                    }
                    catch (Exception ex)
                    {
                        // If any property is unavailable, treat as not inherited and log the issue
                        Console.Error.WriteLine($"Warning: Unable to evaluate line inheritance for shape ID {shape.ID} on page '{page.Name}'. {ex.Message}");
                    }

                    // Log shapes where inheritance status differs between fill and line
                    if (isFillInherited != isLineInherited)
                    {
                        Console.WriteLine($"Inconsistent inheritance detected - Page: '{page.Name}', Shape ID: {shape.ID}, NameU: '{shape.NameU}', FillInherited: {isFillInherited}, LineInherited: {isLineInherited}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}