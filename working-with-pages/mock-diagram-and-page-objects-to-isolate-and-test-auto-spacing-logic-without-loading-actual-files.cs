using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path for the generated diagram.
        string outputPath = "AutoSpacedDiagram.vsdx";

        // Ensure the output directory exists; create if missing.
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Guard: ensure we can write to the output path.
        try
        {
            // Create an empty diagram instance (no file loading required).
            Diagram diagram = new Diagram();

            // Create a new page and add it to the diagram.
            Page page = new Page();
            diagram.Pages.Add(page);

            // Add three rectangle shapes to the page for spacing demonstration.
            // DrawRectangle returns a shape ID (long). Use it to retrieve the Shape object.
            long rectId1 = page.DrawRectangle(1, 1, 2, 1); // (pinX, pinY, width, height)
            long rectId2 = page.DrawRectangle(4, 1, 2, 1);
            long rectId3 = page.DrawRectangle(7, 1, 2, 1);

            // Retrieve Shape objects (optional, shown for completeness).
            Shape rect1 = page.Shapes.GetShape(rectId1);
            Shape rect2 = page.Shapes.GetShape(rectId2);
            Shape rect3 = page.Shapes.GetShape(rectId3);

            // Set simple text for each rectangle to identify them.
            rect1.Text.Value.Clear();
            rect1.Text.Value.Add(new Txt("Rect 1"));
            rect2.Text.Value.Clear();
            rect2.Text.Value.Add(new Txt("Rect 2"));
            rect3.Text.Value.Clear();
            rect3.Text.Value.Add(new Txt("Rect 3"));

            // Configure auto-spacing options: 1.5 inches horizontal, 1 inch vertical.
            AutoSpaceOptions spacingOptions = new AutoSpaceOptions
            {
                DistanceInHorizontal = 1.5, // horizontal gap between shapes
                DistanceInVertical = 1.0    // vertical gap between shapes
            };

            // Apply auto-spacing to all shapes on the page.
            page.AutoSpaceShapes(page.Shapes, spacingOptions);
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error during diagram creation or auto-spacing: {ex.Message}");
            return;
        }

        // Save the diagram to the specified file using VSDX format.
        try
        {
            // Load the diagram again (the same instance) and save it.
            // Since the diagram variable is out of scope, recreate it by loading the file is unnecessary.
            // Instead, we re-instantiate the diagram creation steps in a separate block to keep scope.
            // For simplicity, we repeat the creation steps and then save.
            Diagram diagram = new Diagram();
            Page page = new Page();
            diagram.Pages.Add(page);
            long rectId1 = page.DrawRectangle(1, 1, 2, 1);
            long rectId2 = page.DrawRectangle(4, 1, 2, 1);
            long rectId3 = page.DrawRectangle(7, 1, 2, 1);
            Shape rect1 = page.Shapes.GetShape(rectId1);
            Shape rect2 = page.Shapes.GetShape(rectId2);
            Shape rect3 = page.Shapes.GetShape(rectId3);
            rect1.Text.Value.Clear(); rect1.Text.Value.Add(new Txt("Rect 1"));
            rect2.Text.Value.Clear(); rect2.Text.Value.Add(new Txt("Rect 2"));
            rect3.Text.Value.Clear(); rect3.Text.Value.Add(new Txt("Rect 3"));
            AutoSpaceOptions spacingOptions = new AutoSpaceOptions
            {
                DistanceInHorizontal = 1.5,
                DistanceInVertical = 1.0
            };
            page.AutoSpaceShapes(page.Shapes, spacingOptions);

            // Save the diagram with the auto-spaced layout.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during saving.
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}