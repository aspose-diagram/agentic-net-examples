using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeThemeComparison
{
    static void Main()
    {
        try
        {

            // Load the original diagram
            Diagram originalDiagram = new Diagram("input.vsdx");

            // Load a diagram that contains the desired theme
            Diagram themeDiagram = new Diagram("theme.vsdx");

            // Get the first shape on the first page (adjust index as needed)
            Shape targetShape = originalDiagram.Pages[0].Shapes[0];

            // Render the shape before applying the theme
            string beforeImagePath = "shape_before.png";
            ImageSaveOptions beforeOptions = new ImageSaveOptions(SaveFileFormat.Png);
            targetShape.ToImage(beforeImagePath, beforeOptions);

            // Apply the theme from the theme diagram to the original diagram
            originalDiagram.CopyTheme(themeDiagram);

            // After applying the theme, retrieve the same shape (its ID remains the same)
            Shape themedShape = originalDiagram.Pages[0].Shapes[0];

            // Render the shape after applying the theme
            string afterImagePath = "shape_after.png";
            ImageSaveOptions afterOptions = new ImageSaveOptions(SaveFileFormat.Png);
            themedShape.ToImage(afterImagePath, afterOptions);

            // Optionally, save the modified diagram (not required for thumbnail comparison)
            originalDiagram.Save("output_with_theme.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
