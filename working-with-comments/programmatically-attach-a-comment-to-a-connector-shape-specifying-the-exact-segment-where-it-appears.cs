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

        // Guard: ensure input file exists
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
                // Find the first connector (1-D shape) in the diagram
                Shape connectorShape = null;
                Page connectorPage = null;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Connector shapes are 1-D (OneD == true)
                        if (shape.OneD)
                        {
                            connectorShape = shape;
                            connectorPage = page;
                            break;
                        }
                    }
                    if (connectorShape != null)
                        break;
                }

                if (connectorShape == null || connectorPage == null)
                {
                    Console.Error.WriteLine("No connector shape found in the diagram.");
                    return;
                }

                // Determine a point on the connector segment.
                // For a 1-D connector, the PinX/PinY represent the midpoint of the line.
                double commentX = connectorShape.XForm.PinX.Value;
                double commentY = connectorShape.XForm.PinY.Value;

                // Add a page-level comment at the calculated coordinates.
                // This comment will appear on the specific segment of the connector.
                string commentText = "Comment attached to connector segment";
                connectorPage.AddComment(commentX, commentY, commentText);

                // Save the modified diagram to the output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}