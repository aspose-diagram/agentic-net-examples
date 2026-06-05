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

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define the desired font name and line spacing value
            string targetFont = "Arial";
            double lineSpacing = 12.0; // line spacing in points (or appropriate unit)

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains text
                    if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.ToString()))
                    {
                        // Set line spacing for each paragraph in the shape
                        foreach (Aspose.Diagram.Para para in shape.Paras)
                        {
                            para.SpLine.Value = lineSpacing;
                        }

                        // Set the font for each character run in the shape
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            ch.FontName.Value = targetFont;
                        }
                    }
                }
            }

            // Save the modified diagram as a DOCX file.
            // Aspose.Diagram does not have a native DOCX format, so we save as VSDX with a .docx extension.
            string outputPath = "output.docx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
