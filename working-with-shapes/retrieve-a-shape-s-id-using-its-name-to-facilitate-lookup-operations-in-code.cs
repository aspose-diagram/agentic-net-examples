using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeName>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool shapeFound = false;

                // Iterate through all pages and shapes to locate the shape by name.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Compare the shape's NameU (universal name) with the target name.
                        if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Shape ID is a long value.
                            long shapeId = shape.ID;
                            Console.WriteLine($"Shape found. Name: {targetShapeName}, ID: {shapeId}");
                            shapeFound = true;
                            break;
                        }
                    }

                    if (shapeFound)
                        break;
                }

                if (!shapeFound)
                {
                    Console.WriteLine($"Shape with name \"{targetShapeName}\" was not found in the diagram.");
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