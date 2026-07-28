using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramConnectorExport
{
    // Simple DTO for JSON serialization
    public class ConnectorInfo
    {
        public long ShapeId { get; set; }
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output JSON file path
                string outputPath = "connectors.json";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // List to hold connector data
                List<ConnectorInfo> connectors = new List<ConnectorInfo>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Retrieve start and end coordinates from XForm1D
                            double startX = shape.XForm1D.BeginX.Value;
                            double startY = shape.XForm1D.BeginY.Value;
                            double endX = shape.XForm1D.EndX.Value;
                            double endY = shape.XForm1D.EndY.Value;

                            connectors.Add(new ConnectorInfo
                            {
                                ShapeId = shape.ID,
                                StartX = startX,
                                StartY = startY,
                                EndX = endX,
                                EndY = endY
                            });
                        }
                    }
                }

                // Serialize the list to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(connectors, jsonOptions);

                // Write JSON to file
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Exported {connectors.Count} connectors to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}