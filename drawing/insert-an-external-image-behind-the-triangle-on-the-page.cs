using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file path, image file path, output PNG path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <imagePath> <outputPngPath>");
            return;
        }

        string diagramPath = args[0];
        string imagePath = args[1];
        string outputPath = args[2];

        // Guard against missing diagram file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard against missing image file
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Work with the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Calculate center position for the image
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Insert the external image as a new shape covering the whole page
            long imageShapeId;
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                // AddShape returns the shape ID (long)
                imageShapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, fs);
            }

            // Retrieve the newly added image shape
            Shape imageShape = page.Shapes.GetShape(imageShapeId);

            // Send the image shape to the back so it appears behind all other shapes (including the triangle)
            imageShape.SendToBack();

            // Optionally lock the background image to prevent accidental selection
            imageShape.Protection.LockSelect.Value = BOOL.True;

            // Refresh the diagram to apply changes
            diagram.Refresh();

            // Prepare PNG save options
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the modified diagram as PNG
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram saved with background image to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}