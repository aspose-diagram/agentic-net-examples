using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Identify the shape to check (replace with actual ID or lookup logic)
            long shapeId = 1; // example shape ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Determine if the shape allows dynamic glue (gluing-enabled)
            bool canGlue = shape.Misc.GlueType.Value == GlueTypeValue.AllowDynamicGlue;

            if (canGlue)
            {
                // Add a dynamic connector shape to the diagram
                // Note: AddShape expects an int for the isCalculate flag (0 = false, 1 = true)
                long connectorId = diagram.AddShape(0, 0, "Dynamic connector", 0);

                // Connect the original shape to itself (replace with desired target shape IDs)
                page.ConnectShapesViaConnector(
                    shapeId,
                    ConnectionPointPlace.Bottom,
                    shapeId,
                    ConnectionPointPlace.Top,
                    connectorId);

                Console.WriteLine("Connector attached successfully.");
            }
            else
            {
                Console.WriteLine("Shape is not gluing-enabled; connector not attached.");
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}