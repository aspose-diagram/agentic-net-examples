using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputVisio> <outputVisio> <shapeId> <txtPinX> <txtPinY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string txtPinXArg = args[3];
        string txtPinYArg = args[4];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Parse shape ID and desired TxtPin coordinates.
            if (!long.TryParse(shapeIdArg, out long shapeId))
            {
                Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
                return;
            }

            if (!double.TryParse(txtPinXArg, out double txtPinX))
            {
                Console.Error.WriteLine($"Invalid TxtPinX value: {txtPinXArg}");
                return;
            }

            if (!double.TryParse(txtPinYArg, out double txtPinY))
            {
                Console.Error.WriteLine($"Invalid TxtPinY value: {txtPinYArg}");
                return;
            }

            // Retrieve the first page (or adjust as needed).
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the target shape by its ID.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                return;
            }

            // Ensure the shape is not deleted.
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId} is marked as deleted.");
                return;
            }

            // Set custom text pin coordinates.
            shape.TextXForm.TxtPinX.Value = txtPinX; // Horizontal offset of the text block origin.
            shape.TextXForm.TxtPinY.Value = txtPinY; // Vertical offset of the text block origin.

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}