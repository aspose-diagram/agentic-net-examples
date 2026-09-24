using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Iterate through all pages and shapes to find connector shapes (1‑D shapes)
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Filter only connector shapes
                    if (shape.OneD)
                    {
                        // Display current jump style
                        Console.WriteLine($"Connector ID {shape.ID} current jump style: {shape.Layout.ConLineJumpStyle.Value}");

                        // Set a specific jump style (e.g., Arc) for preview
                        shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;

                        // Optionally, set jump code to always apply jumps
                        shape.Layout.ConLineJumpCode.Value = ConLineJumpCodeValue.Always;
                    }
                }
            }

            // Save a preview image (PNG) to verify the connector jumps visually
            string previewImagePath = "preview.png";
            ImageSaveOptions previewOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(previewImagePath, previewOptions);
            Console.WriteLine($"Preview image saved to: {previewImagePath}");

            // After verification, save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
