using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, config file path, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagram> <configFile> <outputDiagram>");
            return;
        }

        string diagramPath = args[0];
        string configPath = args[1];
        string outputPath = args[2];

        // Guard clauses for file existence
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Read shape IDs from configuration file (one ID per line)
        List<long> shapesToRetainFill = new List<long>();
        try
        {
            foreach (string line in File.ReadAllLines(configPath))
            {
                if (long.TryParse(line.Trim(), out long id))
                {
                    shapesToRetainFill.Add(id);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration file: {ex.Message}");
            return;
        }

        // Load the diagram and process shapes
        try
        {
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if this shape ID is listed in the config
                    if (shapesToRetainFill.Contains(shape.ID))
                    {
                        // ----- Retain custom fill (make fill values explicit) -----
                        // Assign current fill values back to themselves to break inheritance
                        shape.Fill.FillForegnd.Value = shape.Fill.FillForegnd.Value;
                        shape.Fill.FillBkgnd.Value = shape.Fill.FillBkgnd.Value;
                        shape.Fill.FillPattern.Value = shape.Fill.FillPattern.Value;
                        shape.Fill.FillForegndTrans.Value = shape.Fill.FillForegndTrans.Value;
                        shape.Fill.FillBkgndTrans.Value = shape.Fill.FillBkgndTrans.Value;

                        // ----- Inherit line styles (set line cells to INHERITED()) -----
                        // Use the Ufe.F property to force inheritance
                        shape.Line.LineColor.Ufe.F = "INHERITED()";
                        shape.Line.LineWeight.Ufe.F = "INHERITED()";
                        shape.Line.LinePattern.Ufe.F = "INHERITED()";
                        shape.Line.BeginArrow.Ufe.F = "INHERITED()";
                        shape.Line.EndArrow.Ufe.F = "INHERITED()";
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}