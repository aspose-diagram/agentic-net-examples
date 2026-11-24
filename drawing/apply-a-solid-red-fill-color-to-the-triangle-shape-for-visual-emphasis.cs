using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file exists
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
                // Assume the triangle shape is on the first page
                Page page = diagram.Pages[0];

                // Find the first shape whose master name is "Triangle"
                Shape triangleShape = null;
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shp = page.Shapes[i];
                    if (shp.Master != null && string.Equals(shp.Master.Name, "Triangle", StringComparison.OrdinalIgnoreCase))
                    {
                        triangleShape = shp;
                        break;
                    }
                }

                // If not found, fallback to the first shape on the page
                if (triangleShape == null && page.Shapes.Count > 0)
                {
                    triangleShape = page.Shapes[0];
                }

                if (triangleShape == null)
                {
                    Console.Error.WriteLine("No shapes found in the diagram to apply fill.");
                    return;
                }

                // Apply solid fill pattern
                triangleShape.Fill.FillPattern.Value = 1; // Solid fill

                // Set foreground fill color to solid red
                triangleShape.Fill.FillForegnd.Value = "#FF0000";

                // Ensure fill is fully opaque
                triangleShape.Fill.FillForegndTrans.Value = 0.0;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}