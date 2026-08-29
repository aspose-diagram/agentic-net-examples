using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file – adjust as needed.
        string diagramPath = "input.vsdx";

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Unique identifier of the shape to retrieve – adjust as needed.
        long targetShapeId = 5L;

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the first page (index 0) – most diagrams have at least one page.
            Page page = diagram.Pages[0];

            // Attempt to get the shape with the given ID from the page's shape collection.
            Shape shape = page.Shapes.GetShape(targetShapeId);

            // Guard: shape may be null if the ID does not exist on this page.
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found on page '{page.Name}'.");
                return;
            }

            // Output some useful information about the retrieved shape.
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Shape Name: {shape.Name}");
            Console.WriteLine($"Universal Name: {shape.NameU}");
            Console.WriteLine($"Shape Type: {shape.Type}");
            Console.WriteLine($"Master Name: {(shape.Master != null ? shape.Master.Name : "None")}");

            // Example of further processing: display the shape's text (if any).
            string plainText = shape.Text.Value.ToString();
            if (!string.IsNullOrWhiteSpace(plainText))
            {
                Console.WriteLine($"Shape Text: {plainText}");
            }
            else
            {
                Console.WriteLine("Shape contains no text.");
            }
        }
        catch (Exception ex)
        {
            // Report any Aspose.Diagram errors or unexpected exceptions.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}