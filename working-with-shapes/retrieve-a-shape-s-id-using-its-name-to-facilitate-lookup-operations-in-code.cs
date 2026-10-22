using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeName>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool shapeFound = false;
                long foundShapeId = 0L;

                // Iterate through all pages in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Compare the shape's NameU (universal name) with the target name.
                        if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            foundShapeId = shape.ID;
                            shapeFound = true;
                            break;
                        }
                    }

                    if (shapeFound)
                    {
                        break;
                    }
                }

                if (shapeFound)
                {
                    Console.WriteLine($"Shape \"{targetShapeName}\" found with ID: {foundShapeId}");
                }
                else
                {
                    Console.WriteLine($"Shape \"{targetShapeName}\" was not found in the diagram.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}