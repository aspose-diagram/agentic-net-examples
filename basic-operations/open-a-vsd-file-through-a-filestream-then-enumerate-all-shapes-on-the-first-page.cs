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
            Console.Error.WriteLine("Please provide the path to a VSD file as the first argument.");
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
                // Load the diagram inside a using block as required by the API
                using (Diagram diagram = new Diagram(fs))
                {
                    // Ensure the diagram contains at least one page
                    if (diagram.Pages.Count == 0)
                    {
                        Console.WriteLine("The diagram contains no pages.");
                        return;
                    }

                    // Get the first page (index 0)
                    Page firstPage = diagram.Pages[0];

                    Console.WriteLine($"Enumerating shapes on page: {firstPage.NameU}");

                    // Iterate through all shapes on the first page
                    foreach (Shape shape in firstPage.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Output basic shape information
                        Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}