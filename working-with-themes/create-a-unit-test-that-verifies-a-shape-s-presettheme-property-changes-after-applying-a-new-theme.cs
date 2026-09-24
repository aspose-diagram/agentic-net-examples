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

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the first page (page index 0)
            // Parameters: pinX, pinY, master name, page index
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Apply the first preset theme
            try
            {
                shape.PresetTheme = PresetThemeValue.Bubble;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to set first preset theme.", ex);
            }

            // Apply a different preset theme
            try
            {
                shape.PresetTheme = PresetThemeValue.Clouds;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to change to a different preset theme.", ex);
            }

            // If no exception was thrown, we consider the theme change successful
            Console.WriteLine("Shape PresetTheme property changed successfully.");

            // Save the diagram to verify that the changes persist in the file
            diagram.Save("ThemeChangeTest.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved as ThemeChangeTest.vsdx");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
