using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <csvPath> <outputVsdxPath>");
            return;
        }

        string csvPath = args[0];
        string outputPath = args[1];

        // Guard against missing CSV file.
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Read lane definitions from the CSV file.
        string[] lines = File.ReadAllLines(csvPath);
        List<string> laneNames = new List<string>();
        foreach (string line in lines)
        {
            string trimmed = line.Trim();
            if (trimmed.Length == 0)
                continue; // Skip empty lines.

            // Use the first column as the lane name.
            string[] parts = trimmed.Split(',');
            laneNames.Add(parts[0]);
        }

        try
        {
            // Create a new diagram inside a using block as required.
            using (Diagram diagram = new Diagram())
            {
                // Add a window to avoid accessing an empty Windows collection.
                Window window = new Window();
                window.WindowType = WindowTypeValue.Drawing;
                window.WindowState = WindowStateValue.Maximized;
                window.WindowWidth = 1100;
                window.WindowHeight = 700;
                diagram.Windows.Add(window);

                // Add a single page to host the swimlane shapes.
                Page page = new Page();
                diagram.Pages.Add(page);

                // Layout parameters for the lanes.
                double startX = 5.0;          // Left margin (in inches).
                double startY = 5.0;          // Top margin (in inches).
                double laneWidth = 8.0;       // Width of each lane (in inches).
                double laneHeight = 2.0;      // Height of each lane (in inches).
                double currentY = startY + laneHeight / 2.0; // Center Y of the first lane.

                // Create a rectangle shape for each lane and assign its name.
                foreach (string laneName in laneNames)
                {
                    // Draw a rectangle representing the lane.
                    long shapeId = page.DrawRectangle(startX, currentY, laneWidth, laneHeight);

                    // Retrieve the shape object using the returned ID.
                    Shape shape = page.Shapes.GetShape((int)shapeId);

                    // Replace any existing text with the lane name.
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(laneName));

                    // Move to the next vertical position.
                    currentY += laneHeight;
                }

                // Save the diagram as VSDX.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during diagram creation or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}