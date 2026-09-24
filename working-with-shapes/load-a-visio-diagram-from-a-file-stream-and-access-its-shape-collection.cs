using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the path to the Visio file.
        string visioFilePath = "input.vsdx";

        // Verify that the file exists before proceeding.
        if (!File.Exists(visioFilePath))
        {
            Console.Error.WriteLine($"File not found: {visioFilePath}");
            return;
        }

        // Load the Visio diagram from a file stream inside a try/catch block.
        try
        {
            // Open a read-only file stream for the Visio file.
            using (FileStream stream = new FileStream(visioFilePath, FileMode.Open, FileAccess.Read))
            {
                // Initialize the Diagram object using the stream.
                Diagram diagram = new Diagram(stream);

                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Output the page name for context.
                    Console.WriteLine($"Page: {page.Name}");

                    // Access the shape collection of the current page.
                    ShapeCollection shapes = page.Shapes;

                    // Iterate through each shape and display basic information.
                    foreach (Shape shape in shapes)
                    {
                        // Print shape ID and name (if available).
                        Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any exceptions to the error console.
            Console.Error.WriteLine($"Error loading Visio diagram: {ex.Message}");
        }
    }
}