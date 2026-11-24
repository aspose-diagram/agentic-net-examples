using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Locate the first connector shape (OneD returns a native bool)
                Shape connector = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD) // true for 1‑D connector shapes
                    {
                        connector = shape;
                        break;
                    }
                }

                if (connector == null)
                {
                    Console.Error.WriteLine("No connector shape found on the first page.");
                    return;
                }

                // Change the connector appearance to a curved style
                connector.SetConnectorsType(ConnectorsTypeValue.CurvedLines);

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Connector style changed to curved and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}