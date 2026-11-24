using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - input Visio file path
        // args[1] - output Visio file path
        // args[2] - template shape name (NameU)
        // args[3] - comma‑separated list of target shape names (NameU)
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <templateShapeName> <targetShapeNamesCommaSeparated>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string templateShapeName = args[2];
        string[] targetShapeNames = args[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the template shape (first occurrence across all pages)
            Shape templateShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == templateShapeName)
                    {
                        templateShape = shape;
                        break;
                    }
                }
                if (templateShape != null) break;
            }

            if (templateShape == null)
            {
                Console.Error.WriteLine($"Template shape \"{templateShapeName}\" not found.");
                return;
            }

            // Iterate over each target shape name and copy connection points
            foreach (string targetNameRaw in targetShapeNames)
            {
                string targetName = targetNameRaw.Trim();
                if (string.IsNullOrEmpty(targetName))
                    continue;

                // Find the target shape (first match across all pages)
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == targetName)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Target shape \"{targetName}\" not found. Skipping.");
                    continue;
                }

                // Clear existing connections on the target shape (optional – comment out if you want to keep them)
                targetShape.Connections.Clear();

                // Copy each connection point from the template shape to the target shape
                foreach (Connection tmplConn in templateShape.Connections)
                {
                    // Create a new connection and copy the X/Y formulas
                    Connection newConn = new Connection();
                    newConn.X.Ufe.F = tmplConn.X.Ufe.F;
                    newConn.Y.Ufe.F = tmplConn.Y.Ufe.F;
                    targetShape.Connections.Add(newConn);
                }

                Console.WriteLine($"Copied {templateShape.Connections.Count} connection point(s) to shape \"{targetName}\".");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}