using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Use LINQ to select connector (1‑D) shapes on the current page.
                    var connectorShapes = page.Shapes
                        .Cast<Shape>()
                        .Where(shape => shape.OneD) // OneD is a native bool.
                        .ToList();

                    // Set each connector's routing style to orthogonal (right‑angle).
                    foreach (Shape connector in connectorShapes)
                    {
                        // Apply orthogonal routing to the connector.
                        connector.SetConnectorsType(ConnectorsTypeValue.RightAngle);
                    }
                }

                // Save the modified diagram to the specified output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}