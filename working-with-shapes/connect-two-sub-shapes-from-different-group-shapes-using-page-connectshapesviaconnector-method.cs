using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page.
                Page page = diagram.Pages[0];

                // Find the first two group shapes on the page.
                Shape groupShape1 = null;
                Shape groupShape2 = null;

                foreach (Shape shape in page.Shapes)
                {
                    // Detect a group shape via the Type property.
                    if (shape.Type == TypeValue.Group)
                    {
                        if (groupShape1 == null)
                        {
                            groupShape1 = shape;
                        }
                        else if (groupShape2 == null)
                        {
                            groupShape2 = shape;
                            break; // Found both groups.
                        }
                    }
                }

                if (groupShape1 == null || groupShape2 == null)
                {
                    Console.Error.WriteLine("Could not find two group shapes on the page.");
                    return;
                }

                // Retrieve a sub‑shape (first child) from each group.
                Shape subShape1 = null;
                foreach (Shape child in groupShape1.Shapes)
                {
                    subShape1 = child;
                    break;
                }

                Shape subShape2 = null;
                foreach (Shape child in groupShape2.Shapes)
                {
                    subShape2 = child;
                    break;
                }

                if (subShape1 == null || subShape2 == null)
                {
                    Console.Error.WriteLine("One of the groups does not contain any sub‑shapes.");
                    return;
                }

                // Add a dynamic connector shape to the page.
                // Parameters: pinX, pinY, width, height, master name, isCalculate.
                long connectorId = page.AddShape(0, 0, 0, 0, "Dynamic connector", false);

                // Connect the two sub‑shapes via the connector.
                page.ConnectShapesViaConnector(
                    subShape1.ID,
                    ConnectionPointPlace.Bottom,
                    subShape2.ID,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}