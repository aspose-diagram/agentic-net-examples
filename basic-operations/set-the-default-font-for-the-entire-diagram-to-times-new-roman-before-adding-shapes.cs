using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path for the generated diagram
        string outputPath = "output.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // -----------------------------------------------------------------
            // Configure font handling before any diagram operations
            // -----------------------------------------------------------------
            // Set the fallback/default font to Times New Roman
            FontConfigs.DefaultFontName = "Times New Roman";

            // Add the system font folder (non‑recursive) so the renderer can locate fonts
            FontConfigs.SetFontFolder(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), false);

            // -----------------------------------------------------------------
            // Create a new empty diagram
            // -----------------------------------------------------------------
            Diagram diagram = new Diagram();

            // -----------------------------------------------------------------
            // Add a new blank page to the diagram
            // -----------------------------------------------------------------
            Page newPage = new Page();
            // Assign a unique page ID (Visio IDs start from 1)
            newPage.ID = diagram.Pages.Count > 0 ? diagram.Pages[diagram.Pages.Count - 1].ID + 1 : 1;
            diagram.Pages.Add(newPage);

            // -----------------------------------------------------------------
            // Add a rectangle shape to the new page
            // -----------------------------------------------------------------
            // Parameters: pinX, pinY, masterName, isCalculate (bool)
            long shapeId = newPage.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the shape object for further modifications (optional)
            Shape shape = newPage.Shapes.GetShape(shapeId);
            if (shape != null)
            {
                // Example: set the shape's text to "Hello"
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Hello"));
            }

            // -----------------------------------------------------------------
            // Save the diagram with the default font applied
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
