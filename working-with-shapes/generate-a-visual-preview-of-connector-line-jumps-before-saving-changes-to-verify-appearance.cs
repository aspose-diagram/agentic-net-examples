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

            // Paths for the source diagram, preview image, and final saved diagram.
            string sourcePath = "input.vsdx";
            string previewPath = "preview.png";
            string outputPath = "output.vsdx";

            // Load the diagram from file.
            Diagram diagram = new Diagram(sourcePath);

            // Assume we work with the first page.
            Page page = diagram.Pages[0];

            // Iterate through all shapes on the page.
            foreach (Shape shape in page.Shapes)
            {
                // Identify dynamic connector shapes (1‑D shapes with the appropriate master).
                if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                {
                    // Set the connector line jump style to Arc.
                    shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.Arc;

                    // Ensure the jump code is set to Always so the jump appears.
                    shape.Layout.ConLineJumpCode.Value = ConLineJumpCodeValue.Always;
                }
            }

            // Export a visual preview of the diagram (including the updated connector jumps) to PNG.
            ImageSaveOptions previewOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(previewPath, previewOptions);
            Console.WriteLine($"Preview image saved to: {previewPath}");

            // Optional: pause for user verification before final save.
            Console.WriteLine("Press Enter to continue and save the final diagram...");
            Console.ReadLine();

            // Save the modified diagram to VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
