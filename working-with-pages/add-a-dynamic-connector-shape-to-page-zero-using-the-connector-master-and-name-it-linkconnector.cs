using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (page zero)
                Page page = diagram.Pages[0];

                // Add a dynamic connector shape at position (0,0) using the master name "Dynamic connector"
                // The last argument 'isCalculate' must be a BOOL value; use false here
                long connectorId = page.AddShape(0, 0, "Dynamic connector", false);

                // Retrieve the newly added shape object
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Set the shape's name to "LinkConnector"
                connectorShape.NameU = "LinkConnector";
                connectorShape.Name = "LinkConnector";

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Dynamic connector added and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}