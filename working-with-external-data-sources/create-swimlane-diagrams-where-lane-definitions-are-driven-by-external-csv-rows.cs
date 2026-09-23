using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the CSV file containing lane definitions.
        // Expected format per line: LaneName,HeightInInches
        string csvPath = "lanes.csv";

        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Read and parse CSV rows.
        List<(string Name, double Height)> lanes = new List<(string, double)>();
        foreach (string line in File.ReadAllLines(csvPath))
        {
            // Skip empty lines.
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Optional: skip header if it contains non-numeric second column.
            string[] parts = line.Split(',');
            if (parts.Length < 2)
                continue;

            string name = parts[0].Trim();
            if (!double.TryParse(parts[1].Trim(), out double height))
                continue; // Invalid height, skip this row.

            lanes.Add((name, height));
        }

        if (lanes.Count == 0)
        {
            Console.WriteLine("No valid lane definitions found in CSV.");
            return;
        }

        // Create a new empty diagram.
        Diagram diagram = new Diagram();

        // Retrieve the first (and only) page.
        Page page = diagram.Pages[0];

        // Define page dimensions (in inches). Adjust as needed.
        double pageWidth = 11.0;   // e.g., Letter width
        double pageHeight = 8.5;   // e.g., Letter height
        page.PageSheet.PageProps.PageWidth.Value = pageWidth;
        page.PageSheet.PageProps.PageHeight.Value = pageHeight;

        // Margin from top edge (in inches).
        double topMargin = 0.5;
        double currentTop = pageHeight - topMargin;

        // Width of each lane (full page width minus side margins).
        double sideMargin = 0.5;
        double laneWidth = pageWidth - 2 * sideMargin;
        double laneCenterX = pageWidth / 2.0;

        // Create a rectangle for each lane and add its label.
        foreach (var lane in lanes)
        {
            double laneHeight = lane.Height;

            // Calculate the center Y coordinate for the rectangle.
            double centerY = currentTop - laneHeight / 2.0;

            // Draw the lane rectangle.
            long shapeId = page.DrawRectangle(laneCenterX, centerY, laneWidth, laneHeight);

            // Retrieve the shape object.
            Shape laneShape = page.Shapes.GetShape(shapeId);

            // Clear any existing text and add the lane name.
            laneShape.Text.Value.Clear();
            laneShape.Text.Value.Add(new Txt(lane.Name));

            // Optional: set a light fill color for visual distinction.
            laneShape.Fill.FillForegnd.Value = "#E0F7FA";

            // Update the current top position for the next lane.
            currentTop -= laneHeight;
        }

        // Save the diagram to VSDX format.
        string outputPath = "SwimlaneDiagram.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Csv);
        Console.WriteLine($"Swimlane diagram saved to: {outputPath}");
    }
}
