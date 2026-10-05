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
            Console.Error.WriteLine("Usage: Program <path to VSD file>");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Open the VSD file via a FileStream
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream
                Diagram diagram = new Diagram(fs);

                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Retrieve the first page (index 0)
                Page firstPage = diagram.Pages[0];
                Console.WriteLine($"Enumerating shapes on page: {firstPage.NameU}");

                // Iterate over all shapes on the first page
                foreach (Shape shape in firstPage.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Output basic shape information
                    Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}