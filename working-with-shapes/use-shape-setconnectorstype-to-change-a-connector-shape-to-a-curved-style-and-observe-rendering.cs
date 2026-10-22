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
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page (you can adapt this to other pages as needed).
            Page page = diagram.Pages[0];

            // Find the first connector shape (OneD == true) on the page.
            Shape connectorShape = null;
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shape = page.Shapes[i];
                if (shape.OneD) // One‑dimensional shapes are connectors.
                {
                    connectorShape = shape;
                    break;
                }
            }

            if (connectorShape == null)
            {
                Console.Error.WriteLine("No connector shape found on the first page.");
                return;
            }

            // Change the connector appearance to a curved style.
            // Valid values: StraightLines, RightAngle, CurvedLines.
            connectorShape.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Connector style updated and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}