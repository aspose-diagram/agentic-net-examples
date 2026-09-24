using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate argument count.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <shapeId>");
            return;
        }

        // Assign input file path and guard its existence.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Parse shape ID and guard parsing errors.
        if (!long.TryParse(args[1], out long shapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {args[1]}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0) of the diagram.
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID (cast to int as required by GetShape).
            Shape shape = page.Shapes.GetShape((int)shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Move the shape 50 units right by increasing PinX.
            shape.XForm.PinX.Value += 50.0;

            // Move the shape 30 units down by increasing PinY.
            shape.XForm.PinY.Value += 30.0;

            // Prepare output file path (original name with "_moved" suffix).
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_moved.vsdx");

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Inform the user of successful operation.
            Console.WriteLine($"Shape {shapeId} moved and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}