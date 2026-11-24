using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file, config file, output file
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <configPath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard against missing files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Read shape IDs from the configuration file (one ID per line)
            var idsToProcess = new HashSet<long>();
            foreach (var line in File.ReadAllLines(configPath))
            {
                if (long.TryParse(line.Trim(), out long id))
                {
                    idsToProcess.Add(id);
                }
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if this shape is listed in the configuration
                    if (idsToProcess.Contains(shape.ID))
                    {
                        // Retain the existing custom fill (no changes needed)

                        // Set line properties to inherit from parent/style
                        // Use the Ufe.F property to assign the INHERITED() formula
                        shape.Line.LineColor.Ufe.F = "INHERITED()";
                        shape.Line.LineWeight.Ufe.F = "INHERITED()";
                        shape.Line.LinePattern.Ufe.F = "INHERITED()";
                    }
                }
            }

            // Save the modified diagram to the specified output path (VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}