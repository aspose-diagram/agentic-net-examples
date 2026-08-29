using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram that contains the shape to be examined
            Diagram diagram = new Diagram("original.vsdx");

            // Access the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Render the shape before any theme changes
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            shape.ToImage("shape_before.png", imgOptions);

            // Load a diagram that holds the desired theme
            Diagram themeSource = new Diagram("theme_source.vsdx");

            // Apply the theme from the source diagram to the target diagram
            diagram.CopyTheme(themeSource);

            // Re‑acquire the shape after the theme has been applied (the shape instance may have been refreshed)
            shape = diagram.Pages[0].Shapes[0];

            // Render the shape after the theme has been applied
            shape.ToImage("shape_after.png", imgOptions);

            // Optionally save the diagram with the new theme applied
            diagram.Save("original_with_theme.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
