using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define a temporary file path for saving the diagram
        string tempFilePath = Path.ChangeExtension(Path.GetTempFileName(), ".vsdx");

        // Ensure any existing file at the path is removed before proceeding
        if (File.Exists(tempFilePath))
        {
            try { File.Delete(tempFilePath); } catch { /* ignore */ }
        }

        try
        {
            // -------------------- Create a new diagram --------------------
            using (Diagram diagram = new Diagram())
            {
                // Add a window (required for a non-empty diagram)
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

                // -------------------- Add first shape with explicit fill --------------------
                // Add a rectangle shape (master name "Rectangle") and retrieve it
                long shapeId1 = page.AddShape(1, 1, 2, 2, "Rectangle");
                Shape shape1 = page.Shapes.GetShape(shapeId1);
                // Set explicit fill color
                shape1.Fill.FillForegnd.Value = "#FF0000";

                // -------------------- Add second shape that inherits fill --------------------
                long shapeId2 = page.AddShape(4, 1, 2, 2, "Rectangle");
                Shape shape2 = page.Shapes.GetShape(shapeId2);
                // Enable inheritance for the fill foreground
                shape2.Fill.FillForegnd.Ufe.F = "INHERITED()";

                // -------------------- Save the diagram --------------------
                diagram.Save(tempFilePath, SaveFileFormat.Vsdx);
            }

            // -------------------- Reload the saved diagram --------------------
            using (Diagram loadedDiagram = new Diagram(tempFilePath))
            {
                // Retrieve the same page and shapes by their IDs
                Page loadedPage = loadedDiagram.Pages[0];
                Shape loadedShape1 = loadedPage.Shapes.GetShape(1); // shapeId1 is 1 in this simple case
                Shape loadedShape2 = loadedPage.Shapes.GetShape(2); // shapeId2 is 2

                // -------------------- Verify fill inheritance --------------------
                string explicitFill = loadedShape1.Fill.FillForegnd.Value;
                string inheritedFill = loadedShape2.InheritFill.FillForegnd.Value;

                Console.WriteLine($"Explicit fill color: {explicitFill}");
                Console.WriteLine($"Inherited fill color reported: {inheritedFill}");

                // Check if the inherited fill matches the explicit fill
                if (string.Equals(explicitFill, inheritedFill, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Fill inheritance verification succeeded.");
                }
                else
                {
                    throw new Exception("Fill inheritance verification failed: inherited fill does not match explicit fill.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempFilePath))
            {
                try { File.Delete(tempFilePath); } catch { /* ignore */ }
            }
        }
    }
}