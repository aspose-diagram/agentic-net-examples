using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Configuration file path (contains shape IDs, one per line)
        string configPath = "retain_fill_config.txt";
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"File not found: {configPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        // Read shape IDs that should retain their custom fill
        var retainFillIds = new HashSet<long>();
        try
        {
            foreach (var line in File.ReadAllLines(configPath))
            {
                // Skip empty lines and comments
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                    continue;

                if (long.TryParse(line.Trim(), out long id))
                    retainFillIds.Add(id);
                else
                    Console.Error.WriteLine($"Invalid shape ID in config: '{line}'");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading config file: {ex.Message}");
            return;
        }

        // Load the diagram
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

        // Process each shape on every page
        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply inherited line style to all shapes
                    if (shape.InheritLine != null)
                    {
                        shape.Line.LineColor.Value = shape.InheritLine.LineColor.Value;
                        shape.Line.LineWeight.Value = shape.InheritLine.LineWeight.Value;
                        shape.Line.LinePattern.Value = shape.InheritLine.LinePattern.Value;
                        shape.Line.BeginArrow.Value = shape.InheritLine.BeginArrow.Value;
                        shape.Line.EndArrow.Value = shape.InheritLine.EndArrow.Value;
                    }

                    // If the shape is NOT listed in the config, also inherit its fill
                    if (!retainFillIds.Contains(shape.ID))
                    {
                        if (shape.InheritFill != null)
                        {
                            shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                            shape.Fill.FillBkgnd.Value = shape.InheritFill.FillBkgnd.Value;
                            shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                        }
                    }
                    // Shapes listed in retainFillIds keep their existing fill (no action needed)
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing shapes: {ex.Message}");
            return;
        }

        // Save the modified diagram
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