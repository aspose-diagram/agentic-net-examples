using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Guard: ensure the directory for the output file exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Configure the default font for all diagrams before any diagram is created
            FontConfigs.DefaultFontName = "Times New Roman";

            // Optionally add the system font folder (non‑recursive)
            FontConfigs.SetFontFolder(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), false);

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new blank page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Draw a rectangle shape at position (2,2) with width and height of 2 units
            long shapeId = page.DrawRectangle(2.0, 2.0, 2.0, 2.0);

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text (if any) and add new text to the shape
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample Shape"));

            // Save the diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}