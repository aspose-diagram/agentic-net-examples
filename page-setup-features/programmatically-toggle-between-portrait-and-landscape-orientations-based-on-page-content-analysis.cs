using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Initialize bounds for content analysis.
                    double minX = double.MaxValue;
                    double maxX = double.MinValue;
                    double minY = double.MaxValue;
                    double maxY = double.MinValue;

                    // Examine every shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape geometry.
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Calculate shape extents.
                        double left = pinX - width / 2.0;
                        double right = pinX + width / 2.0;
                        double bottom = pinY - height / 2.0;
                        double top = pinY + height / 2.0;

                        // Update overall page bounds.
                        if (left < minX) minX = left;
                        if (right > maxX) maxX = right;
                        if (bottom < minY) minY = bottom;
                        if (top > maxY) maxY = top;
                    }

                    // If no shapes were found, keep the existing orientation.
                    if (minX == double.MaxValue || minY == double.MaxValue)
                    {
                        continue;
                    }

                    // Determine content width and height.
                    double contentWidth = maxX - minX;
                    double contentHeight = maxY - minY;

                    // Choose orientation based on content dimensions.
                    if (contentWidth > contentHeight)
                    {
                        // Set page orientation to Landscape.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }
                    else
                    {
                        // Set page orientation to Portrait.
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with updated orientations to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}