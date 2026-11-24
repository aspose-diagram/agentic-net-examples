using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes to find connectors
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Identify connector shapes (1-D shapes with a layout)
                if (shape.OneD && shape.Layout != null && shape.Geoms != null && shape.Geoms.Count > 0)
                {
                    // Count the number of line segments in the first geometry
                    int segmentCount = 0;
                    try
                    {
                        // According to API, LineToCol holds the line-to points
                        segmentCount = shape.Geoms[0].CoordinateCol.LineToCol.Count;
                    }
                    catch
                    {
                        // If the expected collections are not present, skip this shape
                        continue;
                    }

                    // Calculate bends (segments - 1)
                    int bends = segmentCount - 1;

                    // Apply reroute option only if bends > 3
                    if (bends > 3)
                    {
                        shape.Layout.ConFixedCode.Value = ConFixedCodeValue.RerouteFreely;
                    }
                }
            }
        }

        try
        {
            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}