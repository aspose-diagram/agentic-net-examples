using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Expected theme identifier
        const PresetThemeValue expectedTheme = PresetThemeValue.Bubble;
        const string expectedThemeText = "Bubble";

        // Create a new diagram and apply theme
        try
        {
            Diagram diagram = new Diagram();

            // Add a new page
            diagram.Pages.Add(new Page());

            // Get the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape using a built‑in master name
            // The AddShape method returns the shape ID (long)
            long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", false);

            // Retrieve the shape object
            Shape shape = page.Shapes.GetShape(shapeId);

            // Apply the preset theme to the shape
            shape.PresetTheme = expectedTheme;

            // Store the theme identifier in the shape's text for later verification
            shape.Text.Value.Add(new Txt(expectedThemeText));

            // Save the diagram to a VSDX file
            const string outputPath = "theme_test_output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Verify the file was created before loading
            if (!File.Exists(outputPath))
            {
                Console.Error.WriteLine($"File not found: {outputPath}");
                return;
            }

            // Load the saved diagram
            Diagram loadedDiagram = new Diagram(outputPath);

            // Retrieve the same shape from the loaded diagram
            Page loadedPage = loadedDiagram.Pages[0];
            Shape loadedShape = loadedPage.Shapes.GetShape(shapeId);

            // Extract the stored theme text
            string loadedThemeText = loadedShape.Text.Value.ToString();

            // Validate that the stored theme matches the expected theme
            if (loadedThemeText != expectedThemeText)
            {
                throw new Exception($"Theme validation failed. Expected: {expectedThemeText}, Loaded: {loadedThemeText}");
            }
            else
            {
                Console.WriteLine($"Theme validation succeeded. Theme '{loadedThemeText}' was correctly applied and persisted.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}