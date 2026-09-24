using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramInheritanceDemo
{
    // Represents a single shape inheritance configuration entry.
    public class InheritanceConfig
    {
        public long ShapeId { get; set; }
        public bool InheritFill { get; set; }
        public bool InheritLine { get; set; }
    }

    public class Program
    {
        // Entry point: args[0] = input diagram path, args[1] = JSON config path, args[2] = output diagram path
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: DiagramInheritanceDemo <inputDiagram> <configJson> <outputDiagram>");
                return;
            }

            string diagramPath = args[0];
            string jsonPath = args[1];
            string outputPath = args[2];

            // Load JSON configuration
            InheritanceConfig[] configs;
            try
            {
                string jsonContent = File.ReadAllText(jsonPath);
                configs = JsonSerializer.Deserialize<InheritanceConfig[]>(jsonContent);
                if (configs == null)
                {
                    throw new Exception("Failed to deserialize JSON configuration.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON configuration: {ex.Message}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            // Apply inheritance flags to each specified shape
            foreach (InheritanceConfig cfg in configs)
            {
                bool shapeFound = false;

                // Search all pages for the shape with the given ID
                foreach (Page page in diagram.Pages)
                {
                    Shape shape = null;
                    try
                    {
                        shape = page.Shapes.GetShape(cfg.ShapeId);
                    }
                    catch
                    {
                        // GetShape throws if not found; ignore and continue
                    }

                    if (shape != null)
                    {
                        shapeFound = true;

                        // Apply fill inheritance if requested
                        if (cfg.InheritFill)
                        {
                            // Copy inherited fill foreground color
                            shape.Fill.FillForegnd.Value = shape.InheritFill.FillForegnd.Value;
                            // Copy inherited fill pattern
                            shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                        }

                        // Apply line inheritance if requested
                        if (cfg.InheritLine)
                        {
                            // Copy inherited line color
                            shape.Line.LineColor.Value = shape.InheritLine.LineColor.Value;
                            // Copy inherited line weight
                            shape.Line.LineWeight.Value = shape.InheritLine.LineWeight.Value;
                            // Copy inherited line pattern
                            shape.Line.LinePattern.Value = shape.InheritLine.LinePattern.Value;
                        }

                        // No further processing needed for this shape
                        break;
                    }
                }

                if (!shapeFound)
                {
                    Console.WriteLine($"Warning: Shape with ID {cfg.ShapeId} not found in the diagram.");
                }
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving diagram: {ex.Message}");
            }
        }
    }
}