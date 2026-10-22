using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "triangle.vsdx";

        try
        {
            // Create a new empty diagram (contains one default page)
            Diagram diagram = new Diagram();

            // Add a window to the diagram (required for proper rendering/saving)
            Window window = new Window();
            window.WindowType = WindowTypeValue.Drawing;
            window.WindowState = WindowStateValue.Maximized;
            window.WindowWidth = 1100;
            window.WindowHeight = 700;
            diagram.Windows.Add(window);

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define triangle vertices (closed polygon: repeat first point at the end)
            double[] trianglePoints = new double[]
            {
                1.0, 1.0,   // Point 1
                5.0, 1.0,   // Point 2
                3.0, 4.0,   // Point 3
                1.0, 1.0    // Close back to Point 1
            };

            // Draw the triangle on the page
            page.DrawPolyline(trianglePoints);

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}