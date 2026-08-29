using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string filePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve a shape (example: shape with ID 1)
                Shape shape = page.Shapes.GetShape(1);

                // Get the line color as a hex string (e.g., "#FF0000")
                string lineColorHex = shape.Line.LineColor.Value;

                if (string.IsNullOrEmpty(lineColorHex))
                {
                    Console.WriteLine("Line color is not set for the shape.");
                    return;
                }

                // Remove leading '#' if present
                if (lineColorHex.StartsWith("#"))
                    lineColorHex = lineColorHex.Substring(1);

                // Handle short hex format like "F00"
                if (lineColorHex.Length == 3)
                {
                    lineColorHex = string.Concat(
                        lineColorHex[0], lineColorHex[0],
                        lineColorHex[1], lineColorHex[1],
                        lineColorHex[2], lineColorHex[2]);
                }

                if (lineColorHex.Length != 6)
                {
                    Console.WriteLine($"Unexpected line color format: {shape.Line.LineColor.Value}");
                    return;
                }

                // Parse RGB components
                int r = Convert.ToInt32(lineColorHex.Substring(0, 2), 16);
                int g = Convert.ToInt32(lineColorHex.Substring(2, 2), 16);
                int b = Convert.ToInt32(lineColorHex.Substring(4, 2), 16);

                // Output the RGB values
                Console.WriteLine($"Line color RGB: R={r}, G={g}, B={b}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }