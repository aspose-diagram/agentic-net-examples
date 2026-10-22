using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                try
                {
                    // Access the first page of the diagram.
                    Page page = diagram.Pages[0];

                    // Iterate through shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the shape again via GetShape as mandated.
                        long shapeId = shape.ID;
                        Shape targetShape = page.Shapes.GetShape(shapeId);

                        // Enable gradient fill (pattern 25 corresponds to gradient).
                        targetShape.Fill.FillPattern.Value = 25;

                        // Turn on the gradient.
                        targetShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

                        // Clear any existing gradient stops.
                        targetShape.Fill.GradientFill.GradientStops.Clear();

                        // Add a new gradient stop at position 0.75 with blue color (#0000FF).
                        targetShape.Fill.GradientFill.GradientStops.Add(
                            new DoubleValue(0.75, MeasureConst.NUM),
                            new ColorValue("#0000FF", MeasureConst.Undefined));

                        // Process only the first applicable shape.
                        break;
                    }

                    // Save the modified diagram to the specified output path.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
        }
    }
}