using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define temporary file path for the test diagram
        string tempPath = "fill_inheritance_test.vsdx";

        // Ensure any existing file is removed to start fresh
        if (File.Exists(tempPath))
        {
            File.Delete(tempPath);
        }

        // Variable to hold the shape ID for later verification
        int shapeId = -1;

        try
        {
            // Create a new empty diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Draw a simple rectangle shape; returns a long shape ID
            long shapeIdLong = page.DrawRectangle(pinX: 2f, pinY: 2f, width: 2f, height: 1f);

            // Convert long ID to int for GetShape (expects int)
            shapeId = (int)shapeIdLong;

            // Retrieve the shape object to modify its fill properties
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set a solid fill pattern (1 = solid) and a foreground color
            shape.Fill.FillPattern.Value = 1;               // Solid fill
            shape.Fill.FillForegnd.Value = "#FF0000";       // Red fill

            // Save the diagram to the temporary file using VSDX format
            diagram.Save(tempPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during creation or saving
            Console.Error.WriteLine($"Error during diagram creation/saving: {ex.Message}");
            return;
        }

        // Verify that the file was created successfully
        if (!File.Exists(tempPath))
        {
            Console.Error.WriteLine($"File not found after save: {tempPath}");
            return;
        }

        try
        {
            // Reload the diagram from the saved file
            Diagram loadedDiagram = new Diagram(tempPath);

            // Access the same page and shape by the stored ID
            Page loadedPage = loadedDiagram.Pages[0];
            Shape reloadedShape = loadedPage.Shapes.GetShape(shapeId);

            // Verify that the explicit fill color persisted correctly
            if (reloadedShape.Fill.FillForegnd.Value != "#FF0000")
            {
                throw new Exception("Fill color was not persisted correctly after reload.");
            }

            // Verify that the inherited fill matches the explicit fill
            if (reloadedShape.InheritFill.FillForegnd.Value != "#FF0000")
            {
                throw new Exception("Inherited fill color does not match expected value after reload.");
            }

            // If both checks pass, report success
            Console.WriteLine("Fill inheritance verification passed.");
        }
        catch (Exception ex)
        {
            // Report any errors that occurred during loading or verification
            Console.Error.WriteLine($"Error during verification: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file (optional)
            if (File.Exists(tempPath))
            {
                try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}