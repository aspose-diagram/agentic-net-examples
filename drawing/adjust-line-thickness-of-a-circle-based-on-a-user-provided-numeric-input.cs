using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the input Visio file path
        Console.Write("Enter the path to the Visio file (e.g., diagram.vsdx): ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the input file exists
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine("Error: The specified input file does not exist.");
            return;
        }

        // Prompt user for the desired line thickness (in inches)
        Console.Write("Enter the line thickness (in inches, e.g., 0.03): ");
        string thicknessInput = Console.ReadLine();

        // Parse the thickness value
        if (!double.TryParse(thicknessInput, out double lineThickness) || lineThickness <= 0)
        {
            Console.Error.WriteLine("Error: Invalid line thickness value.");
            return;
        }

        // Prompt user for the output Visio file path
        Console.Write("Enter the path for the output Visio file (e.g., output.vsdx): ");
        string outputPath = Console.ReadLine();

        // Guard: ensure the output directory exists
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            Console.Error.WriteLine("Error: Output path is empty.");
            return;
        }
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine("Error: The directory for the output file does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (assuming the circle is on the first page)
            Page page = diagram.Pages[0];

            // Retrieve the shape with ID 1 (commonly the first shape; adjust as needed)
            Shape shape = page.Shapes.GetShape(1);

            // Ensure the shape is not deleted
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine("Error: The target shape is marked as deleted.");
                return;
            }

            // Adjust the line thickness (line weight) of the shape
            shape.Line.LineWeight.Value = lineThickness;

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Line thickness updated and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}