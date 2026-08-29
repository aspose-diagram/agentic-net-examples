using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (use first argument if provided, otherwise default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (use second argument if provided, otherwise default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Use LINQ to select all connector shapes (1‑D shapes)
                var connectorShapes = page.Shapes.Cast<Shape>().Where(s => s.OneD);

                // Set each connector's routing style to orthogonal (right‑angle)
                foreach (Shape connector in connectorShapes)
                {
                    try
                    {
                        connector.SetConnectorsType(ConnectorsTypeValue.RightAngle);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to set connector type for shape ID {connector.ID}: {ex.Message}");
                    }
                }
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading, processing, or saving
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}