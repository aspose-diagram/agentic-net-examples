using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the uniform scaling factor
                const double scaleFactor = 1.5;

                // Iterate through all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the page
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify the triangle shape by its master name
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            // Uniformly scale width and height
                            shape.XForm.Width.Value *= scaleFactor;
                            shape.XForm.Height.Value *= scaleFactor;

                            // Optionally, keep the shape centered by leaving PinX/PinY unchanged
                            // (Visio automatically keeps the shape centered when width/height change)

                            Console.WriteLine($"Scaled triangle on page {pageIndex + 1}, shape ID {shape.ID}.");
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}