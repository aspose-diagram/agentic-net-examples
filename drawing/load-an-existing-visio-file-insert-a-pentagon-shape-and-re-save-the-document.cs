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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                return;
            }

            // Get the first page where the pentagon will be inserted.
            Page page = diagram.Pages[0];

            // Define the vertices of a pentagon (in inches). The first point is repeated at the end to close the shape.
            double[] pentagonPoints = new double[]
            {
                5.0, 3.0,   // Point 1
                7.0, 4.5,   // Point 2
                6.5, 6.5,   // Point 3
                3.5, 6.5,   // Point 4
                3.0, 4.5,   // Point 5
                5.0, 3.0    // Close back to Point 1
            };

            // Draw the pentagon on the page. The method returns the shape ID (long).
            long shapeId = page.DrawPolyline(pentagonPoints);

            // Retrieve the newly created shape to modify its properties.
            Shape pentagonShape = page.Shapes.GetShape((int)shapeId);
            if (pentagonShape != null)
            {
                // Set fill foreground color (hex string). Values are in the format "#RRGGBB".
                pentagonShape.Fill.FillForegnd.Value = "#D3D3D3"; // Light gray

                // Ensure the shape is fully opaque (0.0 = opaque, 1.0 = transparent).
                pentagonShape.Fill.FillForegndTrans.Value = 0.0;
            }

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the modified diagram back to a Visio file (VSDX format).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}