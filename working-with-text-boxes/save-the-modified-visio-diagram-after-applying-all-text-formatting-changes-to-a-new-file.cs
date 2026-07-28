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

            // Apply text formatting to all shapes that contain text
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without any text
                    if (string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        continue;

                    // Replace existing text with a new formatted run
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Formatted Text"));

                    // Clear any existing character formatting
                    shape.Chars.Clear();

                    // Define character formatting (font, color, size, style)
                    Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                    ch.IX = 0; // start index of the character run
                    ch.FontName.Value = "Calibri";
                    ch.Color.Value = "#FF0000"; // red text
                    ch.Size.Value = 12.0 / 72.0; // 12 pt expressed in inches
                    ch.Style.Value = StyleValue.Bold; // bold style

                    // Apply the character formatting to the shape
                    shape.Chars.Add(ch);
                }
            }

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
