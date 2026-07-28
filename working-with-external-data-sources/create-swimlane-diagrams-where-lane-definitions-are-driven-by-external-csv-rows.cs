using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the CSV file that defines swimlane rows
        string csvPath = "lanes.csv";
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"File not found: {csvPath}");
            return;
        }

        // Path for the generated Visio diagram
        string outputPath = "SwimlaneDiagram.vsdx";

        try
        {
            // Load all CSV lines (skip empty lines)
            string[] lines = File.ReadAllLines(csvPath);
            // Create a new empty diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Retrieve the first (and only) page
            Page page = diagram.Pages[0];

            // Iterate over each CSV line to create a lane
            foreach (string rawLine in lines)
            {
                // Trim whitespace and ignore blank lines
                string line = rawLine.Trim();
                if (string.IsNullOrEmpty(line))
                    continue;

                // Expected CSV format: LaneName,PinX,PinY,Width,Height,FillColorHex
                // Example: "Marketing,1,5,4,2,#FFCC00"
                string[] parts = line.Split(',');
                if (parts.Length < 6)
                {
                    Console.Error.WriteLine($"Invalid CSV format: {line}");
                    continue;
                }

                // Parse lane name
                string laneName = parts[0].Trim();

                // Parse numeric values with invariant culture
                if (!double.TryParse(parts[1].Trim(), out double pinX) ||
                    !double.TryParse(parts[2].Trim(), out double pinY) ||
                    !double.TryParse(parts[3].Trim(), out double width) ||
                    !double.TryParse(parts[4].Trim(), out double height))
                {
                    Console.Error.WriteLine($"Numeric parsing failed for line: {line}");
                    continue;
                }

                // Parse fill color (hex string, e.g., "#FFCC00")
                string fillColor = parts[5].Trim();
                if (!fillColor.StartsWith("#"))
                {
                    Console.Error.WriteLine($"Invalid color format for line: {line}");
                    continue;
                }

                // Draw a rectangle representing the swimlane
                long shapeId = page.DrawRectangle(pinX, pinY, width, height);

                // Retrieve the shape object using the returned ID
                Shape laneShape = page.Shapes.GetShape(shapeId);

                // Apply fill color to the lane shape
                laneShape.Fill.FillForegnd.Value = fillColor;

                // Clear any existing text and add the lane name
                laneShape.Text.Value.Clear();
                laneShape.Text.Value.Add(new Txt(laneName));

                // Optional: center the text horizontally and vertically
                laneShape.TextXForm.TxtLocPinX.Value = 0.5; // center horizontally
                laneShape.TextXForm.TxtLocPinY.Value = 0.5; // center vertically
            }

            // Save the diagram to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Swimlane diagram created successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}