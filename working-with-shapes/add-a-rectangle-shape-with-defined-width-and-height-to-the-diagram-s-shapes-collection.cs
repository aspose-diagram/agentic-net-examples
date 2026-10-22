using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Output file path (optional command‑line argument)
        string outputPath = args.Length > 0 ? args[0] : "AddedRectangle.vsdx";

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one window (required for saving)
            if (diagram.Windows.Count == 0)
            {
                Window window = new Window
                {
                    WindowType = WindowTypeValue.Drawing,
                    WindowState = WindowStateValue.Maximized,
                    WindowWidth = 1100,
                    WindowHeight = 700
                };
                diagram.Windows.Add(window);
            }

            // Use the active page for adding the shape
            Page page = diagram.ActivePage;

            // Define rectangle center position and desired size (in inches)
            double pinX = 5.0;          // X coordinate of the shape's center
            double pinY = 5.0;          // Y coordinate of the shape's center
            double rectWidth = 2.0;     // Width in inches
            double rectHeight = 1.0;    // Height in inches

            // Add a rectangle shape using the built‑in "Rectangle" master.
            // The fourth parameter (isCalculate) is a plain bool.
            long shapeId = page.AddShape(pinX, pinY, "Rectangle", false);

            // Retrieve the newly added shape by its ID
            Shape rectangle = page.Shapes.GetShape(shapeId);

            // Set the shape's width and height
            rectangle.XForm.Width.Value = rectWidth;
            rectangle.XForm.Height.Value = rectHeight;

            // Optionally set a simple fill color (hex string) and solid fill pattern
            rectangle.Fill.FillForegnd.Value = "#FFCC00"; // Light orange fill
            rectangle.Fill.FillPattern.Value = 1;        // 1 = solid pattern

            // Save the diagram to the specified file in VSDX format
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