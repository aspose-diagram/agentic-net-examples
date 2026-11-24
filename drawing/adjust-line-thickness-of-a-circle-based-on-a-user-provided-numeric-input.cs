using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for the input Visio file path
        Console.Write("Enter the path to the input Visio file: ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: The file \"{inputPath}\" does not exist.");
            return;
        }

        // Prompt for the output Visio file path
        Console.Write("Enter the desired output file path (e.g., output.vsdx): ");
        string outputPath = Console.ReadLine();

        // Prompt for the line thickness (in inches)
        Console.Write("Enter the line thickness (in inches, e.g., 0.03): ");
        string thicknessInput = Console.ReadLine();

        if (!double.TryParse(thicknessInput, out double lineThickness) || lineThickness <= 0)
        {
            Console.Error.WriteLine("Error: Invalid line thickness value.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (assumes at least one page exists)
                Page page = diagram.Pages[0];

                // Define simple coordinates for the circle
                double pinX = 1.0;   // X coordinate of the lower-left corner
                double pinY = 1.0;   // Y coordinate of the lower-left corner
                double width = 2.0;  // Width of the ellipse (circle)
                double height = 2.0; // Height of the ellipse (circle)

                // Draw an ellipse (circle) on the page; returns the shape ID
                long shapeId = page.DrawEllipse(pinX, pinY, width, height);

                // Retrieve the newly created shape by its ID
                Shape circle = page.Shapes.GetShape(shapeId);

                // Adjust the line thickness (weight) of the circle
                circle.Line.LineWeight.Value = lineThickness;

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}