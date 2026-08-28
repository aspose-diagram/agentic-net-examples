using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;

namespace DiagramConnectorExport
{
    // DTO for JSON serialization
    public class ConnectorInfo
    {
        public long Id { get; set; }
        public double StartX { get; set; }
        public double StartY { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                const string inputPath = "input.vsdx";
                const string outputPath = "connectors.json";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    var connectors = new List<ConnectorInfo>();

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify connector shapes (1‑D shapes)
                            if (shape.OneD)
                            {
                                // Ensure XForm1D is available
                                if (shape.XForm1D != null)
                                {
                                    var info = new ConnectorInfo
                                    {
                                        Id = shape.ID,
                                        StartX = shape.XForm1D.BeginX.Value,
                                        StartY = shape.XForm1D.BeginY.Value,
                                        EndX = shape.XForm1D.EndX.Value,
                                        EndY = shape.XForm1D.EndY.Value
                                    };
                                    connectors.Add(info);
                                }
                            }
                        }
                    }

                    // Serialize connector data to JSON with indentation
                    string json = JsonSerializer.Serialize(connectors, new JsonSerializerOptions { WriteIndented = true });

                    // Write JSON to file
                    File.WriteAllText(outputPath, json);

                    Console.WriteLine($"Exported {connectors.Count} connectors to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}