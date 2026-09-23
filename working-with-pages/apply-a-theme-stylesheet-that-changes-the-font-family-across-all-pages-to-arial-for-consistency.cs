using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create a stylesheet that sets the font family to Arial
            StyleSheet arialStyle = new StyleSheet();
            arialStyle.ID = diagram.StyleSheets.Count + 1;
            arialStyle.Name = "ArialFontStyle";

            Aspose.Diagram.Char fontChar = new Aspose.Diagram.Char();
            fontChar.IX = 0; // character index
            fontChar.FontName.Value = "Arial";
            arialStyle.Chars.Add(fontChar);

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(arialStyle);

            // Apply the stylesheet to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                page.ApplyStyle(arialStyle.ID, arialStyle.ID, arialStyle.ID);
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
