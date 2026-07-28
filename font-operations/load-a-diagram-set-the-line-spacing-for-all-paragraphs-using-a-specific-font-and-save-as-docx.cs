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

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output file path – Aspose.Diagram does not support DOCX export,
            // so we save the diagram in its native VSDX format.
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Font to apply to all text characters
            string targetFont = "Calibri";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                    {
                        // Set the font for every character in the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            ch.FontName.Value = targetFont;
                        }

                        // Set line spacing for every paragraph in the shape
                        foreach (Para para in shape.Paras)
                        {
                            // Example: set line spacing to 0.2 inches (adjust as needed)
                            para.SpLine.Value = 0.2;
                        }
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
