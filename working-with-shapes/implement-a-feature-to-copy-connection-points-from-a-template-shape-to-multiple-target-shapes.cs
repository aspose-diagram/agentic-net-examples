using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        // args[0] = input diagram path
        // args[1] = output diagram path
        // args[2] = template shape name (NameU)
        // args[3] = comma‑separated list of target shape names (NameU)
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <templateShapeName> <targetShapeName1,targetShapeName2,...>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string templateShapeName = args[2];
        string targetNamesCsv = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram.
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed).
            Page page = diagram.Pages[0];

            // Locate the template shape by its NameU.
            Shape templateShape = null;
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape s = page.Shapes[i];
                if (s.NameU == templateShapeName)
                {
                    templateShape = s;
                    break;
                }
            }

            if (templateShape == null)
            {
                Console.Error.WriteLine($"Template shape \"{templateShapeName}\" not found on page 0.");
                return;
            }

            // Capture all connection points from the template shape.
            List<Connection> templateConnections = new List<Connection>();
            for (int i = 0; i < templateShape.Connections.Count; i++)
            {
                // Clone the connection definition (X and Y formulas).
                Connection srcConn = templateShape.Connections[i];
                Connection copyConn = new Connection();
                copyConn.X.Ufe.F = srcConn.X.Ufe.F;
                copyConn.Y.Ufe.F = srcConn.Y.Ufe.F;
                templateConnections.Add(copyConn);
            }

            // Split target shape names and process each.
            string[] targetNames = targetNamesCsv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string rawName in targetNames)
            {
                string targetName = rawName.Trim();
                if (string.IsNullOrEmpty(targetName))
                    continue;

                // Find the target shape on the same page.
                Shape targetShape = null;
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape s = page.Shapes[i];
                    if (s.NameU == targetName)
                    {
                        targetShape = s;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Target shape \"{targetName}\" not found on page 0. Skipping.");
                    continue;
                }

                // Add each template connection point to the target shape.
                foreach (Connection tmplConn in templateConnections)
                {
                    Connection newConn = new Connection();
                    newConn.X.Ufe.F = tmplConn.X.Ufe.F;
                    newConn.Y.Ufe.F = tmplConn.Y.Ufe.F;
                    targetShape.Connections.Add(newConn);
                }

                Console.WriteLine($"Copied {templateConnections.Count} connection point(s) to shape \"{targetName}\".");
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}