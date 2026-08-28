using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the CSV file (first argument or default name)
        string csvPath = args.Length > 0 ? args[0] : "lanes.csv";
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"File not found: {csvPath}");
            return;
        }

        // Path for the generated diagram
        string outputPath = "SwimlaneDiagram.vsdx";

        try
        {
            // Read all non‑empty lines from the CSV
            string[] lines = File.ReadAllLines(csvPath);
            // Prepare a list of lane definitions (name, width, fill color)
            var lanes = new System.Collections.Generic.List<(string Name, double Width, string Color)>();
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                // Expected CSV format: Name,WidthInInches,HexColor (e.g., "Sales,2.5,#FFCC00")
                string[] parts = line.Split(',');
                string name = parts[0].Trim();
                double width = parts.Length > 1 && double.TryParse(parts[1], out double w) ? w : 2.0; // default width 2"
                string color = parts.Length > 2 ? parts[2].Trim() : "#FFFFFF"; // default white
                lanes.Add((name, width, color));
            }

            // Create a new empty diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Retrieve the first page to draw on
            Page page = diagram.Pages[0];

            // Page dimensions (in inches) – use default Visio page size
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Horizontal margin from the left edge
            double margin = 0.5; // 0.5"

            // Current X position for the next lane (left edge)
            double currentX = margin;

            // Center Y coordinate for all lanes (vertical centering)
            double centerY = pageHeight / 2.0;

            // Iterate over each lane definition and draw a rectangle
            foreach (var lane in lanes)
            {
                // Compute the center X of the rectangle based on its left edge and width
                double centerX = currentX + lane.Width / 2.0;

                // Draw the rectangle (pinX, pinY, width, height)
                long shapeId = page.DrawRectangle(centerX, centerY, lane.Width, pageHeight - 2 * margin);

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Clear any existing text and add the lane name
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt(lane.Name));

                // Apply the fill color (hex string, e.g., "#FFCC00")
                shape.Fill.FillForegnd.Value = lane.Color;

                // Optional: set a thin black line around the lane
                shape.Line.LineColor.Value = "#000000";
                shape.Line.LineWeight.Value = 0.01; // thin line

                // Update the X position for the next lane
                currentX += lane.Width;
            }

            // Save the diagram as VSDX
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Swimlane diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}