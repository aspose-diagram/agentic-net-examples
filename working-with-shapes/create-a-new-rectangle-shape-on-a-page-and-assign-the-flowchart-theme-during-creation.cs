using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block as required by Aspose.Diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, masterName, isCalculate
                double pinX = 2.0;
                double pinY = 2.0;
                string masterName = "Rectangle";
                bool isCalculate = false;
                long shapeId = page.AddShape(pinX, pinY, masterName, isCalculate);

                // Retrieve the newly added shape using its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Assign a preset theme to the shape.
                // The "Flowchart" preset theme is not available in the current API.
                // Using a known theme as a placeholder; replace with the correct enum when available.
                shape.PresetTheme = PresetThemeValue.Bubble; // TODO: replace with Flowchart theme when supported

                // Save the diagram to the specified file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}