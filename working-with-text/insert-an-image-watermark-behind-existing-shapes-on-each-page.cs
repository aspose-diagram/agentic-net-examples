using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the source diagram and the watermark image.
            string diagramPath = "input.vsdx";
            string watermarkImagePath = "watermark.png";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches).
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark shape.
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add the image as a shape that covers the whole page.
                    using (FileStream imgStream = new FileStream(watermarkImagePath, FileMode.Open, FileAccess.Read))
                    {
                        long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, imgStream);
                        Shape watermarkShape = page.Shapes.GetShape(shapeId);

                        // Send the image to the back so it appears behind other shapes.
                        watermarkShape.SendToBack();

                        // Make the watermark non‑selectable.
                        watermarkShape.Protection.LockSelect.Value = BOOL.True;
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Watermark added and diagram saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
