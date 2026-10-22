using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page (index 0).
                Page page = diagram.Pages[0];

                // Add a new rectangle shape at coordinates (2,2).
                // The fourth argument 'isCalculate' is a bool; false means no immediate recalculation.
                long newShapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the newly added shape using its ID.
                Shape newShape = page.Shapes.GetShape(newShapeId);

                // Assign a unique identifier to the shape.
                // Using the NameU property (user-defined name) for uniqueness.
                newShape.NameU = "UniqueShape_001";
                newShape.Name = "UniqueShape_001";

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Shape has been added and diagram saved successfully.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}