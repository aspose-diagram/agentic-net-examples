using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio VSD file – adjust as needed.
        string filePath = "input.vsd";
        if (!System.IO.File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        // Guard: ensure the file exists before attempting to open.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Open the VSD file via a FileStream.
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream.
                Diagram diagram = new Diagram(fs);

                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Get the first page (index 0).
                Page firstPage = diagram.Pages[0];

                Console.WriteLine($"Enumerating shapes on page: {firstPage.NameU}");

                // Iterate through all shapes on the first page.
                foreach (Shape shape in firstPage.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Output basic shape information.
                    Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}