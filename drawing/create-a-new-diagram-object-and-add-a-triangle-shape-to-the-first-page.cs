using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has at least one window; add one if none exist.
                if (diagram.Windows.Count == 0)
                {
                    Window window = new Window();
                    // Set basic window properties.
                    window.WindowType = WindowTypeValue.Drawing;
                    window.WindowState = WindowStateValue.Maximized;
                    window.WindowWidth = 1100;
                    window.WindowHeight = 700;
                    diagram.Windows.Add(window);
                }

                // Get the first (default) page.
                Page page = diagram.Pages[0];

                // Define points for a triangle (closed polygon).
                // Points: (1,1) -> (5,1) -> (3,4) -> back to (1,1)
                double[] trianglePoints = new double[] { 1, 1, 5, 1, 3, 4, 1, 1 };

                // Draw the triangle on the page using the flat double array overload.
                page.DrawPolyline(trianglePoints);

                // Save the diagram to a file to verify the result.
                string outputPath = "TriangleDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}