using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input file path
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Get the first page (index 0)
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Get a shape by its ID (using 1 as an example)
            // Adjust the ID as needed for your specific diagram
            Shape shape = page.Shapes.GetShape(1);
            if (shape == null)
            {
                Console.Error.WriteLine("Error: Shape with ID 1 not found on the first page.");
                return;
            }

            // Read the line dash style index
            // LinePattern.Value returns a double; cast to int for mapping
            int dashIndex = (int)shape.Line.LinePattern.Value;

            // Map the dash index to a descriptive string
            string dashDescription = dashIndex switch
            {
                0 => "Solid",
                1 => "Dash",
                2 => "Dot",
                3 => "Dash-Dot",
                4 => "Dash-Dot-Dot",
                5 => "Long Dash",
                6 => "Long Dash-Dot",
                7 => "Long Dash-Dot-Dot",
                _ => $"Unknown (index {dashIndex})"
            };

            // Display the result
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Line dash style index: {dashIndex}");
            Console.WriteLine($"Descriptive dash style: {dashDescription}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}