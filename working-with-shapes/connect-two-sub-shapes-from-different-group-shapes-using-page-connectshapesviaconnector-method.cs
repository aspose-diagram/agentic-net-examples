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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0).
                Page page = diagram.Pages[0];

                // Find the first two group shapes on the page.
                Shape groupShape1 = null;
                Shape groupShape2 = null;

                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shp = page.Shapes.GetShape(i);
                    if (shp.Type == TypeValue.Group)
                    {
                        if (groupShape1 == null)
                            groupShape1 = shp;
                        else if (groupShape2 == null)
                        {
                            groupShape2 = shp;
                            break; // Found both groups.
                        }
                    }
                }

                // Ensure we have two groups to work with.
                if (groupShape1 == null || groupShape2 == null)
                {
                    Console.Error.WriteLine("The diagram must contain at least two group shapes.");
                    return;
                }

                // Retrieve the first sub‑shape from each group.
                if (groupShape1.Shapes.Count == 0 || groupShape2.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("One of the groups does not contain any sub‑shapes.");
                    return;
                }

                Shape subShape1 = groupShape1.Shapes[0];
                Shape subShape2 = groupShape2.Shapes[0];

                long subShape1Id = subShape1.ID;
                long subShape2Id = subShape2.ID;

                // Add a dynamic connector shape to the page.
                // The fourth argument (isCalculate) is a bool, not BOOL.
                long connectorId = page.AddShape(0, 0, "Dynamic connector", false);
                Shape connectorShape = page.Shapes.GetShape(connectorId);

                // Connect the two sub‑shapes via the connector.
                // Using ConnectionPointPlace.Bottom for the source and ConnectionPointPlace.Top for the target.
                page.ConnectShapesViaConnector(
                    subShape1Id,
                    ConnectionPointPlace.Bottom,
                    subShape2Id,
                    ConnectionPointPlace.Top,
                    connectorId);

                // Save the modified diagram to the specified output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}