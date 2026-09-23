using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the background image file
            string imagePath = "background.png";
            // Path for the output Visio file
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (foreground page)
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Calculate the center position for the shape (PinX, PinY)
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Insert the image as a shape covering the full page
            long shapeId;
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, fs);
            }

            // Retrieve the newly added shape
            Shape bgShape = page.Shapes.GetShape(shapeId);

            // Set the fill pattern to a texture (value 25) to enable tiling
            bgShape.Fill.FillPattern.Value = 25;

            // Send the shape to the back so it appears behind other content
            bgShape.SendToBack();

            // Lock the shape to prevent selection/editing
            bgShape.Protection.LockSelect.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
