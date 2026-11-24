using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioFilePath = args[0];

        // Guard against missing file.
        if (!File.Exists(visioFilePath))
        {
            Console.Error.WriteLine($"Error: File not found -> {visioFilePath}");
            return;
        }

        try
        {
            // Open the Visio file as a read‑only stream.
            using (FileStream fileStream = new FileStream(visioFilePath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream inside a using block.
                using (Diagram diagram = new Diagram(fileStream))
                {
                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Output basic page information.
                        Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name ?? "<unnamed>"}");

                        // Iterate through each shape on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve basic shape information.
                            long shapeId = shape.ID;
                            string shapeName = shape.Name ?? "<no name>";
                            string masterName = shape.Master != null ? shape.Master.Name : "<no master>";

                            Console.WriteLine($"  Shape ID: {shapeId}, Name: {shapeName}, Master: {masterName}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
