using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Desired output path (DOCX is not a supported format for Aspose.Diagram.
            // The diagram is saved as VSDX instead. Adjust as needed for supported formats.)
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Font to apply to all characters
            const string targetFont = "Arial";

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Set line spacing for each paragraph in the shape
                    for (int p = 0; p < shape.Paras.Count; p++)
                    {
                        // Example: set line spacing factor to 1.0 (single spacing)
                        shape.Paras[p].SpLine.Value = 1.0;
                        // Optional: set space before and after paragraphs
                        shape.Paras[p].SpBefore.Value = 0.0;
                        shape.Paras[p].SpAfter.Value = 0.0;
                    }

                    // Apply the target font to all character runs in the shape
                    for (int c = 0; c < shape.Chars.Count; c++)
                    {
                        shape.Chars[c].FontName.Value = targetFont;
                    }
                }
            }

            // Save the modified diagram.
            // Aspose.Diagram does not support saving directly to DOCX.
            // Use a supported format such as VSDX.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
