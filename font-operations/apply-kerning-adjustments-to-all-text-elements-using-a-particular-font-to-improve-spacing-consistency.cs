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
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // The font name to target for kerning adjustment
            string targetFont = "Calibri";
            // Desired kerning (letter spacing) value in points (1 point = 1/72 inch)
            // Letterspace is measured in 1/20th of a point, so 0.5 point = 10
            double kerningPoints = 0.5;
            double kerningValue = kerningPoints * 20.0; // convert to internal units

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        // Iterate through each character formatting run
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Check if the character run uses the target font
                            if (string.Equals(ch.FontName.Value, targetFont, StringComparison.OrdinalIgnoreCase))
                            {
                                // Apply the kerning adjustment
                                ch.Letterspace.Value = kerningValue;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Kerning adjustment applied and diagram saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
