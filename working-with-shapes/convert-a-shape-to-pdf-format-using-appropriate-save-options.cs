using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input Visio file path and shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];

        // Guard: check if the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse shape ID.
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Get the first page (or you could iterate to find the shape across pages).
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Prepare output PDF path in the same directory as the input file.
            string outputDirectory = Path.GetDirectoryName(inputPath) ?? "";
            string outputFileName = $"Shape_{shapeId}.pdf";
            string outputPath = Path.Combine(outputDirectory, outputFileName);

            // Convert the shape to PDF.
            shape.ToPdf(outputPath);

            Console.WriteLine($"Shape {shapeId} successfully exported to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}