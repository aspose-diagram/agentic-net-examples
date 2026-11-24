using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <ShapeNameToFind>");
            return;
        }

        string visioPath = args[0];
        string targetName = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Variable to hold the found shape.
                Shape foundShape = null;

                // Iterate through all pages and shapes to locate the shape by name (case‑insensitive).
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Compare the shape's Name property ignoring case.
                        if (!string.IsNullOrEmpty(shape.Name) &&
                            string.Equals(shape.Name, targetName, StringComparison.OrdinalIgnoreCase))
                        {
                            foundShape = shape;
                            break;
                        }
                    }

                    if (foundShape != null)
                        break;
                }

                // Output the result.
                if (foundShape != null)
                {
                    Console.WriteLine($"Shape found: ID={foundShape.ID}, Name=\"{foundShape.Name}\"");
                    // The shape is now stored in the variable 'foundShape' and can be used for further processing.
                }
                else
                {
                    Console.WriteLine($"Shape with name \"{targetName}\" was not found in the diagram.");
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