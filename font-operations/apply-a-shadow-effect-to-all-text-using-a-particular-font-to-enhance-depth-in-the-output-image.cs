using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

using Aspose.Drawing.Text; // for font enumeration if needed

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            const string inputPath = "input.vsdx";
            // Path to the output image
            const string outputPath = "output.png";
            // Font name to target for shadow effect
            const string targetFont = "Calibri";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Ensure the shape contains text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        // Check if any character run uses the target font
                        bool usesTargetFont = false;
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            if (ch.FontName.Value != null &&
                                ch.FontName.Value.Equals(targetFont, StringComparison.OrdinalIgnoreCase))
                            {
                                usesTargetFont = true;
                                break;
                            }
                        }

                        if (usesTargetFont)
                        {
                            // Apply a simple shadow to the shape (affects its text)
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;          // Enable shadow
                            shape.Fill.ShdwForegnd.Value = "#000000";                           // Shadow color (black)
                            shape.Fill.ShdwForegndTrans.Value = 0.3;                            // 30% transparency
                            shape.Fill.ShapeShdwOffsetX.Value = 0.05;                           // Horizontal offset
                            shape.Fill.ShapeShdwOffsetY.Value = 0.05;                           // Vertical offset
                        }
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
