using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Optional output path – if not supplied we will overwrite the input.
        string outputPath = args.Length > 1 ? args[1] : inputPath;

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Create default auto‑spacing options.
                    AutoSpaceOptions options = new AutoSpaceOptions();

                    // Perform auto‑spacing on all shapes of the current page.
                    // The method expects a ShapeCollection and AutoSpaceOptions.
                    page.AutoSpaceShapes(page.Shapes, options);

                    // After auto‑spacing, log each shape's new position.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the X and Y coordinates (PinX and PinY) of the shape.
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Output shape identifier and its new position.
                        Console.WriteLine($"Page {page.ID}, Shape ID {shape.ID}: PinX = {pinX}, PinY = {pinY}");
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}