using System;
using System.IO;
using System.Drawing;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "TwoPageTriangleDiagram.vsdx";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a window to the diagram (required for proper rendering)
            Window window = new Window();
            window.WindowType = WindowTypeValue.Drawing;
            window.WindowState = WindowStateValue.Maximized;
            window.WindowWidth = 1100;
            window.WindowHeight = 700;
            diagram.Windows.Add(window);

            // -------------------- Page 1 --------------------
            // Create first page
            Page page1 = new Page();
            page1.Name = "Page1";
            diagram.Pages.Add(page1);

            // Add a triangle shape to page 1 using the built‑in "Triangle" master
            // Parameters: PinX, PinY, Width, Height, MasterName
            long shapeId1 = page1.AddShape(2.0, 2.0, 4.0, 4.0, "Triangle");
            Shape triangle1 = page1.Shapes.GetShape(shapeId1);
            // Set fill color to red
            triangle1.Fill.FillForegnd.Value = "#FF0000";

            // -------------------- Page 2 --------------------
            // Create second page
            Page page2 = new Page();
            page2.Name = "Page2";
            diagram.Pages.Add(page2);

            // Add a triangle shape to page 2
            long shapeId2 = page2.AddShape(3.0, 3.0, 5.0, 5.0, "Triangle");
            Shape triangle2 = page2.Shapes.GetShape(shapeId2);
            // Set fill color to green
            triangle2.Fill.FillForegnd.Value = "#00FF00";

            // Save the diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}