using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the specified Visio file exists.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the provided file path.
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the shape's name; use empty string if null.
                    string shapeName = shape.Name ?? string.Empty;

                    // Count the number of paragraphs in the shape.
                    int paragraphCount = shape.Paras.Count;

                    // Output the shape name and paragraph count.
                    Console.WriteLine($"Shape: {shapeName}, Paragraphs: {paragraphCount}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}