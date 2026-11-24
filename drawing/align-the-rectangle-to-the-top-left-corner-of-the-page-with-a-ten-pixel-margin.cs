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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Work with the first page.
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define a 10‑pixel margin (Visio uses inches; 1 pixel ≈ 1/96 inch).
                double marginInches = 10.0 / 96.0;

                // Find the first rectangle shape (master name "Rectangle").
                Shape rectangle = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a master and compare its name.
                    if (shape.Master != null && 
                        string.Equals(shape.Master.Name, "Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        rectangle = shape;
                        break;
                    }
                }

                if (rectangle == null)
                {
                    Console.Error.WriteLine("No rectangle shape found on the first page.");
                    return;
                }

                // Retrieve the shape's width and height (in inches).
                double shapeWidth = rectangle.XForm.Width.Value;
                double shapeHeight = rectangle.XForm.Height.Value;

                // Compute new PinX and PinY so the rectangle sits at the top‑left
                // corner with the specified margin.
                double halfWidth = shapeWidth / 2.0;
                double halfHeight = shapeHeight / 2.0;

                // Visio's origin (0,0) is at the bottom‑left of the page.
                double newPinX = marginInches + halfWidth;
                double newPinY = pageHeight - marginInches - halfHeight;

                // Apply the new position.
                rectangle.XForm.PinX.Value = newPinX;
                rectangle.XForm.PinY.Value = newPinY;

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}