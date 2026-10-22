using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output image path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputImagePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find a triangle shape
            bool triangleMoved = false;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify triangle by its master name (common master name is "Triangle")
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        // Set the new position (PinX, PinY) to (200, 150)
                        shape.XForm.PinX.Value = 200.0;
                        shape.XForm.PinY.Value = 150.0;

                        triangleMoved = true;
                        // Assuming only one triangle needs to be moved; break out of loops
                        break;
                    }
                }
                if (triangleMoved) break;
            }

            if (!triangleMoved)
            {
                Console.Error.WriteLine("Triangle shape not found in the diagram.");
                return;
            }

            // Prepare PNG save options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Save the modified diagram as a PNG image
            diagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Triangle moved to (200,150) and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}