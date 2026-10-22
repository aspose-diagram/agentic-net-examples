using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a file path argument was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as a command-line argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before attempting to open it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the source file stream for reading and load the diagram directly from the stream
            using (FileStream sourceStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (Diagram diagram = new Diagram(sourceStream)) // mandatory using block for Diagram
            {
                // Iterate through each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    // Retrieve the page by index
                    Page page = diagram.Pages[pageIndex];

                    // Output page information (Page.Name is a plain string, not a cell)
                    string pageName = page.Name ?? "(no name)";
                    Console.WriteLine($"Page {pageIndex + 1}/{diagram.Pages.Count}: {pageName}");

                    // Iterate through each shape on the current page
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        // Retrieve the shape using the integer indexer
                        Shape shape = page.Shapes[shapeIdx];

                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Gather basic shape details (Name is a plain string)
                        string shapeName = shape.Name ?? "(no name)";
                        string masterName = shape.Master != null ? shape.Master.Name ?? "(no master name)" : "(no master)";

                        // Output shape information
                        Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shapeName}, Master: {masterName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}
