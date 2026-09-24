using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file path, shape name, expected shape ID
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <shapeName> <expectedId>");
            return;
        }

        string diagramPath = args[0];
        string targetShapeName = args[1];
        string expectedIdStr = args[2];

        // Verify the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Parse expected ID; if parsing fails, report error and exit
        if (!int.TryParse(expectedIdStr, out int expectedId))
        {
            Console.Error.WriteLine($"Invalid expected ID: {expectedIdStr}");
            return;
        }

        try
        {
            // Load the Visio diagram using Aspose.Diagram
            Diagram diagram = new Diagram(diagramPath);

            // Flag to indicate whether the shape was found
            bool shapeFound = false;

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Compare the shape's universal name (NameU) with the target name
                    if (string.Equals(shape.NameU, targetShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        shapeFound = true; // Mark that we have located the shape

                        // Compare the shape's ID with the expected identifier
                        if (shape.ID == expectedId)
                        {
                            Console.WriteLine($"Success: Shape \"{targetShapeName}\" has the expected ID {expectedId}.");
                        }
                        else
                        {
                            Console.WriteLine($"Mismatch: Shape \"{targetShapeName}\" has ID {shape.ID}, expected {expectedId}.");
                        }

                        // Shape found; no need to continue searching
                        break;
                    }
                }

                if (shapeFound) break; // Exit outer loop if shape already located
            }

            // If shape was not found after scanning all pages, inform the user
            if (!shapeFound)
            {
                Console.WriteLine($"Shape with name \"{targetShapeName}\" was not found in the diagram.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors and write to standard error
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}