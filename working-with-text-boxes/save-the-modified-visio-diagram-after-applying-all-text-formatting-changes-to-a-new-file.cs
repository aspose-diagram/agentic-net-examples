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

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Apply text formatting to every shape on every page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Replace existing text with new content
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Sample Text"));

                    // Define character formatting for the new text
                    shape.Chars.Clear();
                    Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                    ch.IX = 0; // start index of the text run
                    ch.FontName.Value = "Calibri";
                    ch.Size.Value = 12.0 / 72.0; // 12 points expressed in inches
                    ch.Color.Value = "#FF0000"; // red color
                    ch.Style.Value = StyleValue.Bold | StyleValue.Italic; // bold and italic
                    shape.Chars.Add(ch);
                }
            }

            // Save the modified diagram to a new file
            string outputPath = "output_modified.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
