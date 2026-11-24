using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect distinct external data categories from data connections
                HashSet<string> categories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < diagram.DataConnections.Count; i++)
                {
                    var conn = diagram.DataConnections[i];
                    if (!string.IsNullOrWhiteSpace(conn.ConnectionString))
                    {
                        categories.Add(conn.ConnectionString.Trim());
                    }
                }

                // If no categories found, inform the user and skip legend creation
                if (categories.Count == 0)
                {
                    Console.WriteLine("No external data connections found. Legend will not be created.");
                }
                else
                {
                    // Obtain a page to place the legend (use first page or create a new one)
                    Page page = null;
                    if (diagram.Pages.Count > 0)
                    {
                        // Use the first existing page
                        foreach (Page p in diagram.Pages)
                        {
                            page = p;
                            break;
                        }
                    }
                    else
                    {
                        // Create a new page with a unique ID
                        int maxPageId = 0;
                        foreach (Page p in diagram.Pages)
                        {
                            if (p.ID > maxPageId) maxPageId = p.ID;
                        }
                        Page newPage = new Page(maxPageId + 1);
                        diagram.Pages.Add(newPage);
                        page = newPage;
                    }

                    // Define legend positioning and size (in inches)
                    double startX = 1.0;               // Left margin
                    double startY = 1.0;               // Top margin
                    double shapeWidth = 5.0;           // Width of legend box
                    double lineHeight = 0.3;           // Approximate height per line
                    double shapeHeight = lineHeight * categories.Count + 0.2; // Padding

                    // Add a rectangle shape as the legend background using the built‑in "Rectangle" master
                    long legendShapeId = page.AddShape(startX, startY, shapeWidth, shapeHeight, "Rectangle");
                    Shape legendShape = page.Shapes.GetShape(legendShapeId);

                    // Build legend text (each category on a new line)
                    string legendText = string.Join("\n", categories);

                    // Clear any existing text and add the new legend text
                    legendShape.Text.Value.Clear();
                    legendShape.Text.Value.Add(new Txt(legendText));

                    // Set a light fill color for visibility (hex color string)
                    legendShape.Fill.FillForegnd.Value = "#FFFFCC";

                    // Set a thin line weight for the rectangle border
                    legendShape.Line.LineWeight.Value = 0.5;
                }

                // Save the updated diagram to the specified output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with legend to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}