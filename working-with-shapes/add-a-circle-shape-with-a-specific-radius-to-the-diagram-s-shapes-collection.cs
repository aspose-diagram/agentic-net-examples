using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, circle radius
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <radius>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string radiusArg = args[2];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse radius
        if (!double.TryParse(radiusArg, out double radius) || radius <= 0)
        {
            Console.Error.WriteLine("Invalid radius value. It must be a positive number.");
            return;
        }

        // Load the diagram inside a using block (mandatory pattern)
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page to work with
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    // Create and add a new blank page
                    page = new Page();
                    diagram.Pages.Add(page);
                }

                // Determine the center of the page (using page dimensions)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Diameter of the circle
                double diameter = radius * 2.0;

                // Draw an ellipse with equal width and height => a circle
                // DrawEllipse returns the shape ID (long)
                long shapeId = page.DrawEllipse(centerX, centerY, diameter, diameter);

                // Retrieve the newly created shape (optional, shown for completeness)
                Shape circleShape = page.Shapes.GetShape(shapeId);
                // No additional modifications are required; the shape is already a circle

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Circle with radius {radius} added and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}