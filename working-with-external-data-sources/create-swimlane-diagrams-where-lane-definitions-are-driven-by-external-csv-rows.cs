using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class SwimlaneGenerator
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the CSV file that defines the lanes.
            // Expected columns per line: LaneName,Width,Height (in inches)
            string csvPath = "lanes.csv";

            // Path to a Visio stencil or template that contains a Swimlane master shape.
            // Replace {TemplatePath} with the actual file path.
            string stencilPath = "{TemplatePath}";

            // Name of the master shape inside the stencil that represents a swimlane.
            // Replace {MasterName} with the actual master name.
            string masterName = "{MasterName}";

            // Create a new, empty Visio diagram.
            Diagram diagram = new Diagram();

            // Add the swimlane master from the stencil/template to the diagram.
            // The returned masterId is used when adding shapes.
            int masterId = diagram.AddMaster(stencilPath, masterName);

            // Read lane definitions from the CSV file.
            var lanes = new List<(string Name, double Width, double Height)>();
            foreach (var line in File.ReadAllLines(csvPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');
                if (parts.Length < 3)
                    continue; // skip malformed lines

                string name = parts[0].Trim();
                double width = double.Parse(parts[1].Trim());
                double height = double.Parse(parts[2].Trim());

                lanes.Add((name, width, height));
            }

            // Starting position for the first lane (center of the shape).
            double currentX = 1.0; // inches from the left edge
            double currentY = 5.0; // inches from the bottom edge (adjust as needed)

            // Add a swimlane shape for each CSV row.
            foreach (var lane in lanes)
            {
                // AddShape(pinX, pinY, width, height, masterName, masterId)
                // PinX and PinY represent the center of the shape.
                diagram.AddShape(
                    currentX + lane.Width / 2, // PinX
                    currentY,                  // PinY
                    lane.Width,                // Width
                    lane.Height,               // Height
                    masterName,                // Master name
                    masterId);                 // Master ID

                // Advance X position for the next lane.
                currentX += lane.Width;
            }

            // Save the resulting diagram to a VDX file.
            diagram.Save("SwimlaneDiagram.vdx", SaveFileFormat.Vdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
