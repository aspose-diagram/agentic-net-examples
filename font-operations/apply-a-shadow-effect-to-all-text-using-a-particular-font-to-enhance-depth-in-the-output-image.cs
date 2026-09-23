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
            // Path to the output image file
            string outputPath = "output.png";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the target font name (case-sensitive as stored in the diagram)
            string targetFont = "Arial";

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape contains any characters
                    if (shape.Chars == null || shape.Chars.Count == 0)
                        continue;

                    bool usesTargetFont = false;

                    // Examine each character's font name
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        if (ch.FontName != null && ch.FontName.Value == targetFont)
                        {
                            usesTargetFont = true;
                            break;
                        }
                    }

                    // If the shape uses the target font, apply a simple shadow effect
                    if (usesTargetFont)
                    {
                        // Enable simple shadow
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

                        // Set shadow color (dark gray)
                        shape.Fill.ShdwForegnd.Value = "#808080";

                        // Set shadow transparency (30% transparent)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;

                        // Set shadow offsets (in inches)
                        shape.Fill.ShapeShdwOffsetX.Value = 0.05;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.05;
                    }
                }
            }

            // Save the modified diagram as a PNG image
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
