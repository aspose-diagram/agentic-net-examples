using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Path for the modified Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Count gluing relationships (using Connects collection) before modifications
            int glueCountBefore = 0;
            foreach (Page page in diagram.Pages)
            {
                glueCountBefore += page.Connects.Count;
            }
            Console.WriteLine($"Gluing relationships before modification: {glueCountBefore}");

            // --- Begin diagram modifications ---
            // Use the first page for modifications
            Page page0 = diagram.Pages[0];

            // Add a rectangle shape (master name "Rectangle")
            long rectIdLong = page0.AddShape(2.0, 2.0, 1.0, 0.5, "Rectangle");
            Shape rectangle = page0.Shapes.GetShape(rectIdLong);

            // Add a dynamic connector (master name "Dynamic connector")
            long connectorIdLong = page0.AddShape(4.0, 2.0, "Dynamic connector");
            Shape connector = page0.Shapes.GetShape(connectorIdLong);

            // Glue the rectangle's right connection point to the connector's begin point
            page0.GlueShapeToConnectorBeginX(rectIdLong, "Right", connectorIdLong);
            // Glue the rectangle's bottom connection point to the connector's end point
            page0.GlueShapeToConnectorEndX(rectIdLong, "Bottom", connectorIdLong);
            // --- End diagram modifications ---

            // Count gluing relationships after modifications
            int glueCountAfter = 0;
            foreach (Page page in diagram.Pages)
            {
                glueCountAfter += page.Connects.Count;
            }
            Console.WriteLine($"Gluing relationships after modification: {glueCountAfter}");

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}