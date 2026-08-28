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
                // Path for the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the target font name (case-sensitive as stored in the diagram)
                string targetFont = "Calibri";

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape contains text
                        if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            continue;

                        bool fontMatchFound = false;

                        // Check each character run for the target font
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            if (ch.FontName != null && ch.FontName.Value == targetFont)
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
                            // Shadow offset (adjust as needed)
                            shape.Fill.ShapeShdwOffsetX.Value = 0.05;
                            shape.Fill.ShapeShdwOffsetY.Value = 0.05;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }