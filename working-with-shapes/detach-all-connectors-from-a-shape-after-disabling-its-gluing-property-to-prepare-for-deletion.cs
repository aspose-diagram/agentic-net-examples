using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input Visio file path and target shape ID
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <shapeId>");
            return;
        }

        string inputPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse the shape ID (must be a long integer)
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the target shape across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    Shape shape = page.Shapes.GetShape(targetShapeId);
                    if (shape != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {targetShapeId} not found in the diagram.");
                    return;
                }

                // Disable dynamic gluing for the target shape to prevent automatic re‑gluing
                // The GlueType property controls gluing behavior; set it to NoAllowDynamicGlue
                targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

                // Detach all connectors that are glued to the target shape
                // Iterate each page's Connects collection and remove connections referencing the shape
                foreach (Page page in diagram.Pages)
                {
                    var connectsToRemove = new List<Connect>();
                    foreach (Connect conn in page.Connects)
                    {
                        if (conn.FromSheet == targetShapeId || conn.ToSheet == targetShapeId)
                        {
                            connectsToRemove.Add(conn);
                        }
                    }

                    // Remove the collected connections from the page
                    foreach (Connect conn in connectsToRemove)
                    {
                        page.Connects.Remove(conn);
                    }
                }

                // Prepare the output file path (same directory with "_detached" suffix)
                string directory = Path.GetDirectoryName(inputPath);
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_detached.vsdx");

                // Save the modified diagram using the correct overload
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}