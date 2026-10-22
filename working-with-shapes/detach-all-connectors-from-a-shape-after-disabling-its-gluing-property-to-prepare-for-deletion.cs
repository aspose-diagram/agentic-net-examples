using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [shapeId]");
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

        // Parse optional shape ID argument.
        long targetShapeId;
        if (args.Length >= 3 && long.TryParse(args[2], out long parsedId))
        {
            targetShapeId = parsedId;
        }
        else
        {
            Console.Write("Enter the ID of the shape to detach connectors from: ");
            if (!long.TryParse(Console.ReadLine(), out targetShapeId))
            {
                Console.Error.WriteLine("Invalid shape ID.");
                return;
            }
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the target shape on any page.
                Shape targetShape = null;
                Page targetPage = null;

                foreach (Page page in diagram.Pages)
                {
                    // GetShape returns null if the shape ID is not present on this page.
                    Shape shape = page.Shapes.GetShape(targetShapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        targetPage = page;
                        break;
                    }
                }

                if (targetShape == null || targetPage == null)
                {
                    Console.Error.WriteLine($"Shape with ID {targetShapeId} not found in the diagram.");
                    return;
                }

                // Disable dynamic gluing for the target shape.
                // Setting GlueType to NoAllowDynamicGlue prevents further gluing.
                targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

                // Collect all connector connections that involve the target shape.
                List<Connect> connectionsToRemove = new List<Connect>();
                foreach (Connect conn in targetPage.Connects)
                {
                    if (conn.FromSheet == targetShapeId || conn.ToSheet == targetShapeId)
                    {
                        connectionsToRemove.Add(conn);
                    }
                }

                // Remove the collected connections, effectively detaching the connectors.
                foreach (Connect conn in connectionsToRemove)
                {
                    targetPage.Connects.Remove(conn);
                }

                // Save the modified diagram to the specified output path.
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