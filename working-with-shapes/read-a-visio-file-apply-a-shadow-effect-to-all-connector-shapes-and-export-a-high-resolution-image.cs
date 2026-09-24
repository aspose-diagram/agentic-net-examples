using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the exported high‑resolution image
            string outputPath = "output.png";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply a simple shadow to every connector (1‑D shape) in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are 1‑D shapes
                    if (shape.OneD)
                    {
                        // Enable simple shadow
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                        // Shadow color (black)
                        shape.Fill.ShdwForegnd.Value = "#000000";
                        // Shadow transparency (30%)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;
                        // Shadow offset (horizontal and vertical)
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                    }
                }
            }

            // Configure high‑resolution image export (300 DPI PNG)
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.Resolution = 300f;

            // Save the diagram as a high‑resolution image
            diagram.Save(outputPath, imgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
