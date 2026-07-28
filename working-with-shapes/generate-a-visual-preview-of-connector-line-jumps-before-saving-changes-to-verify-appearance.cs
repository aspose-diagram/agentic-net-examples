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

            // Paths for the source diagram, preview image, and final saved diagram
            string sourcePath = "input.vsdx";
            string previewPath = "preview.png";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(sourcePath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Identify connector shapes (1‑D shapes)
                if (shape.OneD)
                {
                    // Set the connector's line jump style to Arc (you can choose other values)
                    shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;

                    // Optionally, you could also set the jump code (e.g., Always) if needed:
                    // shape.Layout.ConLineJumpCode.Value = ConLineJumpCodeValue.Always;
                }
            }

            // Export a visual preview of the diagram (including the updated connector jumps) to PNG
            ImageSaveOptions previewOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(previewPath, previewOptions);

            // Save the modified diagram back to VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
