using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file – replace with your actual file path or pass as an argument.
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Verify that the diagram file exists before attempting to load it.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: The file \"{diagramPath}\" does not exist.");
            return;
        }

        // Prompt the user to enter the shape ID they wish to select.
        Console.Write("Enter the shape ID to retrieve (numeric): ");
        string input = Console.ReadLine();

        if (!long.TryParse(input, out long shapeId))
        {
            Console.Error.WriteLine("Error: Invalid shape ID. Please provide a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram does not contain any pages.");
                return;
            }

            // Access the first page (you can modify this to select a specific page if needed).
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID using the GetShape method.
            Shape shape = page.Shapes.GetShape(shapeId);

            if (shape == null)
            {
                Console.Error.WriteLine($"Error: No shape with ID {shapeId} was found on page \"{page.NameU}\".");
                return;
            }

            // Output some basic information about the retrieved shape.
            Console.WriteLine($"Shape ID: {shape.ID}");
            // NameU holds the universal name of the shape.
            Console.WriteLine($"Shape NameU: {shape.NameU}");
            // Optionally, display whether the shape is logically deleted.
            Console.WriteLine($"Shape Deleted Flag: {(shape.Del == BOOL.True ? "True" : "False")}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}