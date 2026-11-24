using System;
using System.IO;
using System.Drawing;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Output file path
        string outputPath = "TwoPageTriangle.vsdx";

        try
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has at least one window; add one if missing
                if (diagram.Windows.Count == 0)
                {
                    Window window = new Window();
                    window.WindowType = WindowTypeValue.Drawing;
                    window.WindowState = WindowStateValue.Maximized;
                    window.WindowWidth = 1100;
                    window.WindowHeight = 700;
                    diagram.Windows.Add(window);
                }

                // ---------- Page 1 ----------
                Page page1 = new Page();
                page1.Name = "Page-1";
                diagram.Pages.Add(page1);

                // Add a triangle shape to Page 1
                // Parameters: pinX, pinY, width, height, master name
                long triangleId1 = page1.AddShape(2, 2, 4, 4, "Triangle");
                Shape triangle1 = page1.Shapes.GetShape(triangleId1);
                // Set fill color to red
                triangle1.Fill.FillForegnd.Value = "#FF0000";

                // ---------- Page 2 ----------
                Page page2 = new Page();
                page2.Name = "Page-2";
                diagram.Pages.Add(page2);

                // Add a triangle shape to Page 2
                long triangleId2 = page2.AddShape(2, 2, 4, 4, "Triangle");
                Shape triangle2 = page2.Shapes.GetShape(triangleId2);
                // Set fill color to blue
                triangle2.Fill.FillForegnd.Value = "#0000FF";

                // Save the diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram successfully saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}