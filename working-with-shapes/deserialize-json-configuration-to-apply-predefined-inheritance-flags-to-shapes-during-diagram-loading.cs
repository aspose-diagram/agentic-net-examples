using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Represents inheritance settings for a single shape.
    private class ShapeInheritanceConfig
    {
        // Shape identifier (as stored in the Visio file).
        public long ShapeId { get; set; }

        // When true, copy inherited fill values to the shape's own fill cells.
        public bool? InheritFill { get; set; }

        // When true, copy inherited line values to the shape's own line cells.
        public bool? InheritLine { get; set; }
    }

    // Root configuration object that can be extended later.
    private class DiagramConfig
    {
        public List<ShapeInheritanceConfig> Shapes { get; set; } = new();
    }

    static void Main(string[] args)
    {
        // -----------------------------------------------------------------
        // Resolve input arguments: config JSON, source diagram, output diagram.
        // -----------------------------------------------------------------
        string configPath = args.Length > 0 ? args[0] : "config.json";
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"File not found: {configPath}");
            return;
        }

        string diagramPath = args.Length > 1 ? args[1] : "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

        // ---------------------------------------------------------------
        // Deserialize JSON configuration into strongly‑typed objects.
        // ---------------------------------------------------------------
        DiagramConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<DiagramConfig>(json);
            if (config == null)
            {
                Console.Error.WriteLine("Failed to deserialize configuration.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
            return;
        }

        // ---------------------------------------------------------------
        // Load the Visio diagram.
        // ---------------------------------------------------------------
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // ---------------------------------------------------------------
        // Apply inheritance flags to each shape defined in the config.
        // ---------------------------------------------------------------
        foreach (var shapeCfg in config.Shapes)
        {
            // Search all pages for the shape with the specified ID.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                // GetShape throws if the ID does not exist on this page; catch and continue.
                try
                {
                    targetShape = page.Shapes.GetShape(shapeCfg.ShapeId);
                    if (targetShape != null) break;
                }
                catch { /* ignore and continue searching */ }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeCfg.ShapeId} not found.");
                continue;
            }

            // -----------------------------------------------------------
            // If InheritFill flag is set, copy inherited fill cells to the shape.
            // -----------------------------------------------------------
            if (shapeCfg.InheritFill.HasValue && shapeCfg.InheritFill.Value)
            {
                try
                {
                    // Copy foreground, background and pattern values from the inherited fill.
                    targetShape.Fill.FillForegnd.Value = targetShape.InheritFill.FillForegnd.Value;
                    targetShape.Fill.FillBkgnd.Value = targetShape.InheritFill.FillBkgnd.Value;
                    targetShape.Fill.FillPattern.Value = targetShape.InheritFill.FillPattern.Value;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to apply inherited fill to shape {shapeCfg.ShapeId}: {ex.Message}");
                }
            }

            // -----------------------------------------------------------
            // If InheritLine flag is set, copy inherited line cells to the shape.
            // -----------------------------------------------------------
            if (shapeCfg.InheritLine.HasValue && shapeCfg.InheritLine.Value)
            {
                try
                {
                    // Copy line color, weight and pattern from the inherited line.
                    targetShape.Line.LineColor.Value = targetShape.InheritLine.LineColor.Value;
                    targetShape.Line.LineWeight.Value = targetShape.InheritLine.LineWeight.Value;
                    targetShape.Line.LinePattern.Value = targetShape.InheritLine.LinePattern.Value;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to apply inherited line to shape {shapeCfg.ShapeId}: {ex.Message}");
                }
            }
        }

        // ---------------------------------------------------------------
        // Save the modified diagram to the output file.
        // ---------------------------------------------------------------
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}