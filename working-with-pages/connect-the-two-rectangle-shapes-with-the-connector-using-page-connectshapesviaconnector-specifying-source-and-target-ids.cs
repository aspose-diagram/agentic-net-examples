using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Assume we work with the first page.
                Page page = diagram.Pages[0];

                // Find the first two rectangle shapes.
                long? rectId1 = null;
                long? rectId2 = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        if (rectId1 == null)
                            rectId1 = shape.ID;
                        else if (rectId2 == null)
                        {
                            rectId2 = shape.ID;
                            break;
                        }
                    }
                }

                if (rectId1 == null || rectId2 == null)
                {
                    Console.Error.WriteLine("Could not find two rectangle shapes on the page.");
                    return;
                }

                // Find a dynamic connector shape.
                long? connectorId = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Dynamic connector")
                    {
                        connectorId = shape.ID;
                        break;
                    }
                }

                if (connectorId == null)
                {
                    Console.Error.WriteLine("Could not find a dynamic connector shape on the page.");
                    return;
                }

                // Connect the two rectangles via the connector.
                // Using ConnectionPointPlace.Right for source and ConnectionPointPlace.Left for target as an example.
                page.ConnectShapesViaConnector(
                    rectId1.Value,
                    ConnectionPointPlace.Right,
                    rectId2.Value,
                    ConnectionPointPlace.Left,
                    connectorId.Value);

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}