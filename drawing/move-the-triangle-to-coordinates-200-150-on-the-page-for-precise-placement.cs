using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPngPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Flag to indicate whether a triangle was found.
                bool triangleMoved = false;

                // Iterate over all pages explicitly typed.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify a triangle by its master name.
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            // Move the triangle to the desired coordinates (200,150).
                            shape.XForm.PinX.Value = 200.0;
                            shape.XForm.PinY.Value = 150.0;

                            triangleMoved = true;
                            // Assuming only one triangle needs to be moved; break out of loops.
                            break;
                        }
                    }

                    if (triangleMoved)
                        break;
                }

                if (!triangleMoved)
                {
                    Console.Error.WriteLine("No triangle shape was found in the diagram.");
                }

                // Save the modified diagram as PNG.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, pngOptions);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}