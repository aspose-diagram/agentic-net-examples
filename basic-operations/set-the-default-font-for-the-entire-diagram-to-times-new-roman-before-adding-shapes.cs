using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Guard: ensure the directory for the output exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Configure the default font for the entire diagram
            FontConfigs.DefaultFontName = "Times New Roman";

            // Optionally add the system font folder to the font search paths
            FontConfigs.SetFontFolder(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), false);

            // Create a new empty diagram inside a using block
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page; add a blank page if none exist
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                // Get the first page to work with
                Page page = diagram.Pages[0];

                // Add a rectangle shape (master name "Rectangle") at position (2,2)
                // The fourth parameter (isCalculate) is a boolean flag
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the shape object using the returned identifier
                Shape shape = page.Shapes.GetShape(shapeId);

                // Add sample text to the shape; the default font will be Times New Roman
                if (shape != null)
                {
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Sample"));
                }

                // Save the diagram with the default font applied
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}