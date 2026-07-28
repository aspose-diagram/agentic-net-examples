using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace ShapeInheritanceExport
{
    // DTO to hold inheritance information for a shape
    public class ShapeInheritanceInfo
    {
        public long ShapeId { get; set; }
        public string Name { get; set; }
        public string MasterName { get; set; }
        public string FillForegnd { get; set; }
        public string FillBkgnd { get; set; }
        public string LineColor { get; set; }
        public string LineWeight { get; set; }
        public string CharFont { get; set; }
        public string CharSize { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the Visio file to process
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Collect inheritance data for all shapes
                List<ShapeInheritanceInfo> inheritanceData = new List<ShapeInheritanceInfo>();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build DTO for the current shape
                        ShapeInheritanceInfo info = new ShapeInheritanceInfo
                        {
                            ShapeId = shape.ID,
                            Name = shape.Name,
                            MasterName = shape.Master?.Name ?? string.Empty,
                            FillForegnd = shape.InheritFill?.FillForegnd?.Value,
                            FillBkgnd = shape.InheritFill?.FillBkgnd?.Value,
                            LineColor = shape.InheritLine?.LineColor?.Value,
                            LineWeight = shape.InheritLine?.LineWeight?.Value.ToString(),
                            // Retrieve first inherited character formatting if available
                            CharFont = shape.InheritChars?.GetChar(0)?.Font?.Value.ToString(),
                            CharSize = shape.InheritChars?.GetChar(0)?.Size?.Value.ToString()
                        };

                        inheritanceData.Add(info);
                    }
                }

                // Serialize the collection to JSON with indentation for readability
                string json = JsonSerializer.Serialize(inheritanceData, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to a file for external configuration management
                string outputPath = "shapeInheritance.json";
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Shape inheritance data exported to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}