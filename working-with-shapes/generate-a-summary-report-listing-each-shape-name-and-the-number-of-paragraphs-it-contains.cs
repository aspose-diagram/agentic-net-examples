using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path specified.");
            return;
        }

        string filePath = args[0];

        // Guard: verify the Visio file exists.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the shape's name (use NameU; fallback to empty string if null).
                        string shapeName = shape.NameU ?? string.Empty;

                        // Count the number of paragraphs in the shape.
                        int paragraphCount = shape.Paras.Count;

                        // Output the shape name and its paragraph count.
                        Console.WriteLine($"{shapeName}: {paragraphCount}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any exceptions to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}