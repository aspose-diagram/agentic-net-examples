using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: path to the Visio diagram and path to the background image.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <imagePath>");
            return;
        }

        string diagramPath = args[0];
        string imagePath = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Guard: ensure the image file exists.
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"File not found: {imagePath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Read the image bytes once; reuse the same byte array for all pages.
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the center position for the background shape.
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Insert the image as a shape that spans the whole page.
                // The AddShape overload expects a stream containing the image data.
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    // AddShape returns the shape ID (long).
                    long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, ms);
                    // Retrieve the shape object for further configuration.
                    Shape bgShape = page.Shapes.GetShape(shapeId);

                    // Ensure the shape fills the page with the image (no additional fill pattern).
                    bgShape.Fill.FillPattern.Value = 1; // Solid fill.

                    // Send the shape to the back so it appears behind other content.
                    bgShape.SendToBack();

                    // Lock the shape to prevent selection/editing in the UI.
                    bgShape.Protection.LockSelect.Value = BOOL.True;
                }
            }

            // Save the modified diagram back to a new file.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(diagramPath) + "_WithBackground.vsdx");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with background image: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}