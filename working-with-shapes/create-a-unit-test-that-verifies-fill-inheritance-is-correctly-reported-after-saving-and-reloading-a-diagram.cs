using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define temporary file path for the diagram
        string diagramPath = Path.Combine(Path.GetTempPath(), "FillInheritanceTest.vsdx");

        // Ensure any existing file is deleted to start fresh
        if (File.Exists(diagramPath))
        {
            try { File.Delete(diagramPath); } catch { /* ignore */ }
        }

        try
        {
            // -------------------- Create a new diagram --------------------
            Diagram diagram = new Diagram();

            // Add a window (required for a non‑empty diagram)
            Window window = new Window();
            window.WindowType = WindowTypeValue.Drawing;
            window.WindowState = WindowStateValue.Maximized;
            window.WindowWidth = 1100;
            window.WindowHeight = 700;
            diagram.Windows.Add(window);

            // Add a page to the diagram
            Page page = new Page();
            page.Name = "Page-1";
            page.ID = 1;
            diagram.Pages.Add(page);

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name
            long shapeId = page.AddShape(1.0, 1.0, 2.0, 1.0, "Rectangle");
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set a solid fill color
            shape.Fill.FillForegnd.Value = "#FF0000";

            // Enable fill inheritance by assigning the INHERITED() function
            shape.Fill.FillForegnd.Ufe.F = "INHERITED()";

            // Save the diagram to a file
            diagram.Save(diagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram creation/saving: {ex.Message}");
            return;
        }

        try
        {
            // -------------------- Reload the diagram --------------------
            Diagram loadedDiagram = new Diagram(diagramPath);

            // Retrieve the first page and the shape by its ID
            Page loadedPage = loadedDiagram.Pages[0];
            // The shape ID is the same as before because IDs are preserved on save/load
            long shapeId = loadedPage.Shapes[0].ID; // Assuming the first shape is our rectangle
            Shape loadedShape = loadedPage.Shapes.GetShape(shapeId);

            // Compare the fill value with the inherited fill value
            string fillValue = loadedShape.Fill.FillForegnd.Value;
            string inheritFillValue = loadedShape.InheritFill.FillForegnd.Value;

            if (fillValue == inheritFillValue)
            {
                Console.WriteLine("Fill inheritance verification succeeded: inherited fill matches shape fill.");
            }
            else
            {
                throw new Exception($"Fill inheritance verification failed: shape fill '{fillValue}' does not match inherited fill '{inheritFillValue}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram loading/verification: {ex.Message}");
            return;
        }
    }
}