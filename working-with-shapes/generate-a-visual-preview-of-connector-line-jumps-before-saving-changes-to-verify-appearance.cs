using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Paths for preview image and final saved diagram
        string previewPath = "preview.png";
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to modify connector line jumps
            foreach (Page page in diagram.Pages)
            {
                // Loop over shapes collection using indexer (required by API)
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Process only 1-D connector shapes
                    if (shape.OneD)
                    {
                        // Set the line jump style to 'Arc' for visual preview
                        // ConLineJumpStyleValue is the native enumeration for line jumps
                        shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;
                    }
                }
            }

            // ---------- Generate visual preview (PNG) ----------
            // Create image save options for PNG format
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the diagram as a PNG image to preview the connector jumps
            diagram.Save(previewPath, imgOptions);
            Console.WriteLine($"Preview image saved to: {previewPath}");

            // ---------- Save the modified diagram ----------
            // Save the updated diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}