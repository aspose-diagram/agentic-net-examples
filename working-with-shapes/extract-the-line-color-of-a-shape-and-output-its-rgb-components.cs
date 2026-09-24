using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string visioPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            // Get the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Get the first shape on the page (adjust selection logic as needed)
            Shape shape = page.Shapes[0];

            // Retrieve the line color value (hex string, e.g., "#FF00AA")
            string lineColorHex = shape.Line.LineColor.Value;

            if (string.IsNullOrWhiteSpace(lineColorHex) || !lineColorHex.StartsWith("#") || lineColorHex.Length != 7)
            {
                Console.WriteLine("Line color is not defined in a valid hex format.");
                return;
            }

            // Remove the leading '#'
            string hex = lineColorHex.Substring(1);

            // Parse RGB components
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);

            // Output the RGB components
            Console.WriteLine($"Line Color Hex: {lineColorHex}");
            Console.WriteLine($"Red: {r}, Green: {g}, Blue: {b}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
