using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and area threshold (in square inches)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <areaThreshold>");
            return;
        }

        string inputPath = args[0];
        string thresholdArg = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Parse the area threshold
        if (!double.TryParse(thresholdArg, out double areaThreshold) || areaThreshold <= 0)
        {
            Console.Error.WriteLine("Error: Invalid area threshold. Provide a positive numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure width and height cells are present
                    if (shape.XForm?.Width == null || shape.XForm?.Height == null)
                        continue;

                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Calculate shape area (square inches)
                    double area = width * height;

                    // Apply protection if area exceeds the threshold
                    if (area > areaThreshold)
                    {
                        // Lock movement on X and Y axes
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        shape.Protection.LockMoveY.Value = BOOL.True;

                        // Lock resizing (width and height)
                        shape.Protection.LockWidth.Value = BOOL.True;
                        shape.Protection.LockHeight.Value = BOOL.True;

                        // Lock rotation
                        shape.Protection.LockRotate.Value = BOOL.True;

                        // Optionally lock vertex editing
                        shape.Protection.LockVtxEdit.Value = BOOL.True;
                    }
                }
            }

            // Prepare output path (append "_protected" before extension)
            string directory = Path.GetDirectoryName(inputPath) ?? "";
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_protected.vsdx");

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with protections applied to shapes exceeding area {areaThreshold} sq.in.:");
            Console.WriteLine(outputPath);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}