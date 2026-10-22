using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (or a specific page by name if needed).
            // Here we use the first page in the collection.
            Page page = diagram.Pages[0];

            // Retrieve the triangle shape.
            // Assuming the triangle is the first shape (ID = 1). Adjust as necessary.
            Shape triangle = page.Shapes.GetShape(1);

            if (triangle == null)
            {
                Console.Error.WriteLine("Triangle shape with ID 1 was not found on the first page.");
                return;
            }

            // Set solid fill pattern (value 1) to ensure the fill is visible.
            triangle.Fill.FillPattern.Value = 1;

            // Apply solid red fill color using a hex string.
            triangle.Fill.FillForegnd.Value = "#FF0000";

            // Optionally, ensure foreground transparency is fully opaque (0.0 = opaque).
            triangle.Fill.FillForegndTrans.Value = 0.0;

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}