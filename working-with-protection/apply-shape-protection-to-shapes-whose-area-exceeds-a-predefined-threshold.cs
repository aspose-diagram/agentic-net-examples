using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <areaThreshold>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string thresholdArg = args[2];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse the area threshold.
        if (!double.TryParse(thresholdArg, out double areaThreshold))
        {
            Console.Error.WriteLine($"Invalid area threshold: {thresholdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve shape dimensions (in inches).
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Compute the shape's area.
                    double area = width * height;

                    // Apply protection if the area exceeds the threshold.
                    if (area > areaThreshold)
                    {
                        // Lock various aspects of the shape using BOOL enum values.
                        shape.Protection.LockAspect.Value = BOOL.True;
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        shape.Protection.LockMoveY.Value = BOOL.True;
                        shape.Protection.LockWidth.Value = BOOL.True;
                        shape.Protection.LockHeight.Value = BOOL.True;
                        shape.Protection.LockRotate.Value = BOOL.True;
                        shape.Protection.LockVtxEdit.Value = BOOL.True;
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}