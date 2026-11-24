using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Work with the active page
                Page page = diagram.ActivePage;

                // Collect all shapes whose master name is "Diamond"
                List<Shape> diamondShapes = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Diamond")
                    {
                        diamondShapes.Add(shape);
                    }
                }

                // If no diamonds are found, inform the user and exit
                if (diamondShapes.Count == 0)
                {
                    Console.WriteLine("No diamond shapes were found in the diagram.");
                    return;
                }

                // Group the collected diamond shapes
                Shape groupShape = page.Shapes.Group(diamondShapes.ToArray());

                // Export the created group as a single SVG document
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                groupShape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Diamond group exported successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}