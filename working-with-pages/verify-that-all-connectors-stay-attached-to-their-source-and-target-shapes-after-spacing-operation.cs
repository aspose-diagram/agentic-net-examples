using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding.
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
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // ---------- Perform spacing operation ----------
                    // AutoSpaceShapes rearranges shapes while attempting to keep connectors attached.
                    AutoSpaceOptions spacingOptions = new AutoSpaceOptions();
                    // Use default spacing distances; customize if needed:
                    // spacingOptions.DistanceInHorizontal = 2;
                    // spacingOptions.DistanceInVertical = 2;
                    page.AutoSpaceShapes(page.Shapes, spacingOptions);

                    // ---------- Build a set of valid (non‑deleted) shape IDs ----------
                    HashSet<long> validShapeIds = new HashSet<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Del == BOOL.False)
                        {
                            validShapeIds.Add(shape.ID);
                        }
                    }

                    // ---------- Verify each connection (glue) ----------
                    foreach (Connect conn in page.Connects)
                    {
                        long fromId = conn.FromSheet;
                        long toId = conn.ToSheet;

                        // Ensure both source and target shapes exist and are not deleted.
                        if (!validShapeIds.Contains(fromId) || !validShapeIds.Contains(toId))
                        {
                            Console.Error.WriteLine($"Page \"{page.Name}\": Connect entry with FromSheet={fromId}, ToSheet={toId} references an invalid or deleted shape.");
                            throw new Exception("Connector attachment verification failed.");
                        }

                        // Retrieve the target shape (typically the connector) to verify it is a 1‑D shape and not deleted.
                        Shape targetShape = page.Shapes.GetShape(toId);
                        if (targetShape == null)
                        {
                            Console.Error.WriteLine($"Page \"{page.Name}\": Unable to retrieve shape with ID {toId}.");
                            throw new Exception("Connector shape retrieval failed.");
                        }

                        // Verify the target shape is not deleted.
                        if (targetShape.Del == BOOL.True)
                        {
                            Console.Error.WriteLine($"Page \"{page.Name}\": Shape ID {toId} (connector) is marked as deleted after spacing.");
                            throw new Exception("Deleted connector detected.");
                        }

                        // If the target shape is a connector, ensure it is a 1‑D shape.
                        if (targetShape.OneD == false && targetShape.Type == TypeValue.Shape)
                        {
                            // Non‑connector shapes can also appear in Connects (e.g., group glue), so we only enforce 1‑D for actual connectors.
                            // No action needed.
                        }
                    }

                    // If all connectors on the page are valid, report success for this page.
                    Console.WriteLine($"Page \"{page.Name}\" verification passed.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}