using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume the rectangle is on the first page
                Page page = diagram.Pages[0];

                Shape rectangleShape = null;

                // Find the first shape whose master name is "Rectangle"
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        rectangleShape = shape;
                        break;
                    }
                }

                if (rectangleShape == null)
                {
                    Console.Error.WriteLine("No rectangle shape found in the diagram.");
                    return;
                }

                // Apply horizontal mirroring by setting FlipX to BOOL.True via the .Value property
                rectangleShape.XForm.FlipX.Value = BOOL.True;

                // Save the modified diagram to the specified output path using Vsdx format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}