using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <ShapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse the shape identifier.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape identifier '{shapeIdArg}'.");
            return;
        }

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool shapeFound = false;
                int pageIndex = 0; // Manual page counter because Page.Index does not exist.

                // Iterate through all pages to locate the shape with the given ID.
                foreach (Page page in diagram.Pages)
                {
                    try
                    {
                        // Retrieve the shape by its unique identifier.
                        Shape shape = page.Shapes.GetShape(shapeId);

                        // If the shape is found, output its basic information.
                        if (shape != null)
                        {
                            string masterName = shape.Master != null ? shape.Master.Name : "None";
                            Console.WriteLine($"Shape found on page index {pageIndex}:");
                            Console.WriteLine($"  ID          : {shape.ID}");
                            Console.WriteLine($"  Name        : {shape.Name}");
                            Console.WriteLine($"  Master Name : {masterName}");
                            shapeFound = true;
                            break; // Shape IDs are unique across the diagram, so we can stop searching.
                        }
                    }
                    catch (Exception)
                    {
                        // GetShape throws if the ID does not exist on this page; continue searching.
                    }

                    pageIndex++;
                }

                if (!shapeFound)
                {
                    Console.WriteLine($"Shape with ID {shapeId} was not found in the diagram.");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during diagram loading or processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}