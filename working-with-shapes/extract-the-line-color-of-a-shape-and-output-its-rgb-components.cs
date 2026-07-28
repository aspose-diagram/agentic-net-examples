using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Get the first page (you can change the index or use a specific page name)
            Page page = diagram.Pages[0];

            // Retrieve a shape (example: first shape on the page)
            // Adjust the shape ID as needed, e.g., by name or specific ID
            Shape shape = page.Shapes.GetShape(1);

            // Extract the line color value (hex string, e.g., "#FF0000")
            string lineColorHex = shape.Line.LineColor.Value;

            // Ensure the string is in the expected format
            if (string.IsNullOrEmpty(lineColorHex) || !lineColorHex.StartsWith("#") || lineColorHex.Length != 7)
            {
                Console.WriteLine("Line color is not set or has an unexpected format.");
                return;
            }

            // Remove the leading '#'
            string hex = lineColorHex.Substring(1);

            // Parse RGB components from the hex string
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);

            // Output the RGB components
            Console.WriteLine($"Line color RGB: {r}, {g}, {b}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
