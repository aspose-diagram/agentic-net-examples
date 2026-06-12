using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file (can be an existing file or a new empty diagram)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the output Visio file
        string outputPath = "output.vsdx";

        // Load the diagram (if the file does not exist, a new empty diagram will be created)
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            diagram = new Diagram();
        }

        try
        {
            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Page newPage = new Page();
                diagram.Pages.Add(newPage);
            }

            // Use the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            double pinX = 5.0;   // X position in inches
            double pinY = 5.0;   // Y position in inches
            string masterName = "Rectangle";
            long shapeId = page.AddShape(pinX, pinY, masterName);

            // Retrieve the shape instance
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Apply a preset theme to the shape (creates a perspective look)
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Apply 3‑D rotation transformation
            // Rotate 30 degrees around X axis, 20 degrees around Y axis, and 10 degrees around Z axis
            shape.ThreeDFormat.RotationXAngle.Value = 30.0;
            shape.ThreeDFormat.RotationYAngle.Value = 20.0;
            shape.ThreeDFormat.RotationZAngle.Value = 10.0;

            // Choose an oblique rotation type to give a perspective effect
            shape.ThreeDFormat.RotationType.Value = RotationTypeValue.ObliqueFromBottomLeft;

            // Optional: set perspective and distance from ground (default values are fine)
            shape.ThreeDFormat.Perspective.Value = 0.0;
            shape.ThreeDFormat.DistanceFromGround.Value = 0.0;

            // Keep the text flat (not rotated with the shape)
            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with 3‑D rotation applied to the shape.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}