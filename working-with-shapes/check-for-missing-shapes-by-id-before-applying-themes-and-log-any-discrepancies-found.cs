using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string themeSourcePath = "theme.vsdx";
        if (!System.IO.File.Exists(themeSourcePath))
        {
            Console.Error.WriteLine($"File not found: {themeSourcePath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input files
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputPath}");
            return;
        }
        if (!File.Exists(themeSourcePath))
        {
            Console.Error.WriteLine($"Theme source file not found: {themeSourcePath}");
            return;
        }

        try
        {
            // Load the main diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            // Load the theme source diagram
            using (Diagram themeDiagram = new Diagram(themeSourcePath))
            {
                // Define the list of expected shape IDs (example values)
                long[] expectedShapeIds = new long[] { 1, 2, 3, 4, 5 };

                // Iterate over each expected ID and verify its existence
                foreach (long shapeId in expectedShapeIds)
                {
                    // Attempt to retrieve the shape from the first page
                    Shape shape = null;
                    if (diagram.Pages.Count > 0)
                    {
                        // GetShape returns null if the shape does not exist
                        shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                    }

                    // Check if shape is missing or logically deleted
                    if (shape == null || shape.Del == BOOL.True)
                    {
                        Console.WriteLine($"Missing or deleted shape with ID: {shapeId}");
                    }
                }

                // Apply theme from the source diagram to the target diagram
                // Note: The exact API for copying a theme may vary; using CopyTheme as an example.
                // If CopyTheme is not available, replace with appropriate theme application logic.
                try
                {
                    diagram.CopyTheme(themeDiagram);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to copy theme: {ex.Message}");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}