using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output CSV file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the output directory exists.
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Open a StreamWriter for the CSV output.
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    // Write CSV header.
                    writer.WriteLine("ShapeId,Left,Top,Right,Bottom");

                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve shape identifier.
                            long shapeId = shape.ID;

                            // Extract geometric properties from the shape's XForm.
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Calculate bounding rectangle coordinates.
                            double left = pinX - width / 2.0;
                            double right = pinX + width / 2.0;
                            double top = pinY + height / 2.0;
                            double bottom = pinY - height / 2.0;

                            // Write the shape data as a CSV line.
                            writer.WriteLine($"{shapeId},{left},{top},{right},{bottom}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}