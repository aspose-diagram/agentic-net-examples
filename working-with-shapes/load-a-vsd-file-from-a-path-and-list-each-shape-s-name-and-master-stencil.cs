using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input‑vsd‑file-path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – \"{inputPath}\"");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the shape's universal name; fallback to empty string if null.
                        string shapeName = shape.NameU ?? string.Empty;

                        // Retrieve the master (stencil) name if the shape is based on a master.
                        string masterName = shape.Master != null ? shape.Master.Name ?? string.Empty : "None";

                        // Output the shape information.
                        Console.WriteLine($"Page {page.ID} | Shape ID {shape.ID} | Name: {shapeName} | Master: {masterName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}