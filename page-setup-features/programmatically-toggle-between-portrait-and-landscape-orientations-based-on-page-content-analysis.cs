using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Initialize bounding box extremes
                double minX = double.MaxValue;
                double minY = double.MaxValue;
                double maxX = double.MinValue;
                double maxY = double.MinValue;

                // Analyze all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve shape geometry values
                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Calculate shape extents (Visio uses center coordinates)
                    double left = pinX - width / 2.0;
                    double right = pinX + width / 2.0;
                    double top = pinY + height / 2.0;
                    double bottom = pinY - height / 2.0;

                    // Update bounding box
                    if (left < minX) minX = left;
                    if (right > maxX) maxX = right;
                    if (bottom < minY) minY = bottom;
                    if (top > maxY) maxY = top;
                }

                // If no shapes were found, skip orientation change for this page
                if (minX == double.MaxValue || minY == double.MaxValue)
                {
                    Console.WriteLine($"Page '{page.Name}' contains no shapes; orientation unchanged.");
                    continue;
                }

                // Determine content width and height
                double contentWidth = maxX - minX;
                double contentHeight = maxY - minY;

                // Choose orientation based on content aspect ratio
                if (contentWidth > contentHeight)
                {
                    // Set to Landscape
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    Console.WriteLine($"Page '{page.Name}' set to Landscape (content width {contentWidth:F2} > height {contentHeight:F2}).");
                }
                else
                {
                    // Set to Portrait
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    Console.WriteLine($"Page '{page.Name}' set to Portrait (content height {contentHeight:F2} >= width {contentWidth:F2}).");
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}