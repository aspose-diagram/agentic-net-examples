using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                // Skip logically deleted shapes.
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue;

                    // TODO: Apply custom colors to the shape.
                    // Example (replace with actual API calls):
                    // shape.Fill.FillForegnd = "#FF0000"; // Set fill color.
                    // shape.Line.LineColor = "#00FF00"; // Set line color.

                    // TODO: Validate that the shape's ThemeColor is not null.
                    // The actual property name and type may differ; replace with correct API usage.
                    // if (shape.ThemeColor == null)
                    // {
                    //     Console.Error.WriteLine($"Shape ID {shape.ID} on page {page.NameU} has a null ThemeColor.");
                    // }
                }
            }

            Console.WriteLine("Custom colors applied and ThemeColor validation completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}