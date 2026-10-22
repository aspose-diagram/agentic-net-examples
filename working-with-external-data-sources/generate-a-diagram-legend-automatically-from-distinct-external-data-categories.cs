using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be overridden via command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_with_legend.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // 1. Collect distinct external data categories from DataConnections
                // -----------------------------------------------------------------
                HashSet<string> categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < diagram.DataConnections.Count; i++)
                {
                    DataConnection conn = diagram.DataConnections[i];
                    // Use the connection string as a simple category identifier
                    string category = conn.ConnectionString;
                    if (!string.IsNullOrWhiteSpace(category))
                    {
                        categories.Add(category);
                    }
                }

                // If no categories were found, inform the user and skip legend creation
                if (categories.Count == 0)
                {
                    Console.WriteLine("No external data connections found. Legend will not be created.");
                }
                else
                {
                    // -------------------------------------------------------------
                    // 2. Create a legend shape on the first page of the diagram
                    // -------------------------------------------------------------
                    Page firstPage = diagram.Pages[0];

                    // Define legend position (in inches)
                    double legendPosX = 0.5;   // left margin
                    double legendPosY = 0.5;   // top margin

                    // Define legend size (in inches)
                    double legendWidth = 2.5;
                    double legendHeight = 0.2 * categories.Count + 0.2; // height based on number of items

                    // Add a rectangle shape using the built‑in "Rectangle" master.
                    // The AddShape method returns the shape ID (long).
                    long legendShapeId = firstPage.AddShape(legendPosX, legendPosY, "Rectangle");

                    // Retrieve the newly added shape for further configuration
                    Shape legendShape = firstPage.Shapes.GetShape(legendShapeId);

                    // Set the shape's size
                    legendShape.XForm.Width.Value = legendWidth;
                    legendShape.XForm.Height.Value = legendHeight;

                    // Build the legend text: each category on a new line
                    string legendText = "Legend:\n";
                    foreach (string cat in categories)
                    {
                        legendText += $"• {cat}\n";
                    }

                    // Clear any existing text and add the new legend text
                    legendShape.Text.Value.Clear();
                    legendShape.Text.Value.Add(new Txt(legendText));

                    // Optional: set a light fill color for the legend background (hex string)
                    legendShape.Fill.FillForegnd.Value = "#FFFFCC";

                    // Optional: set a thin solid border
                    legendShape.Line.LineWeight.Value = 0.05; // points (in inches)
                    legendShape.Line.LinePattern.Value = LinePatternValue.Solid;
                }

                // -------------------------------------------------------------
                // 3. Save the modified diagram with the legend
                // -------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with legend to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}