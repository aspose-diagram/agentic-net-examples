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
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process only shapes with a solid line (LinePattern value 0)
                    // In Visio, a LinePattern of 0 represents a solid line.
                    if (shape.Line.LinePattern.Value != 0)
                        continue;

                    // At this point, the shape has a solid line.
                    // Iterate through its geometries for further customization.
                    for (int i = 0; i < shape.Geoms.Count; i++)
                    {
                        // Cast the geometry object to Geom
                        Geom geom = (Geom)shape.Geoms[i];

                        // Example customization: set the shape's fill foreground color to red.
                        // This demonstrates processing of the geometry; actual geometry-specific
                        // modifications would depend on the required scenario.
                        shape.Fill.FillForegnd.Value = "#FF0000";
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}