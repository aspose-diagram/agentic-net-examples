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
                // Path for the output image
                string outputPath = "output.png";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Font to target for shadow effect
                string targetFont = "Calibri";

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has text
                        if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            continue;

                        bool fontMatchFound = false;

                        // Check each character run for the target font
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            if (ch.FontName != null && ch.FontName.Value.Equals(targetFont, StringComparison.OrdinalIgnoreCase))
                            {
                                fontMatchFound = true;
                                break;
                            }
                        }

                        // If the shape contains text using the target font, apply shadow to the shape
                        if (fontMatchFound)
                        {
                            // Enable simple shadow
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                            // Shadow color (black)
                            shape.Fill.ShdwForegnd.Value = "#000000";
                            // Shadow transparency (30% transparent)
                            shape.Fill.ShdwForegndTrans.Value = 0.3;
                            // Shadow offset (in inches)
                            shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                            shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                        }
                    }
                }

                // Save the modified diagram as a PNG image
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine("Shadow effect applied and diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }