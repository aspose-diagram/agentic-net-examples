using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram (no LoadOptions needed)
            Diagram diagram = new Diagram(inputPath);

            // Access the first page in the document
            Page page = diagram.Pages[0];

            // Locate the first shape that is not marked as deleted
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.False)
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No editable shape found on the page.");
                return;
            }

            // Store original geometry values for the comment
            double originalWidth = targetShape.XForm.Width.Value;
            double originalHeight = targetShape.XForm.Height.Value;

            // Modify geometry: increase width by 1 inch and height by 0.5 inch
            targetShape.XForm.Width.Value = originalWidth + 1.0;
            targetShape.XForm.Height.Value = originalHeight + 0.5;

            // Create a comment that explains the modification rationale
            string commentText = $"Width changed from {originalWidth} to {targetShape.XForm.Width.Value} inches; " +
                                 $"Height changed from {originalHeight} to {targetShape.XForm.Height.Value} inches " +
                                 $"to accommodate additional content.";

            // Attach the comment to the shape's ShapeSheet
            page.AddComment(targetShape, commentText);

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
