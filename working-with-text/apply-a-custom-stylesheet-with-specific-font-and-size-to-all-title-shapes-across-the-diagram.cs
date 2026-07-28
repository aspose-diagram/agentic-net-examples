using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Create a custom stylesheet for title shapes
            StyleSheet titleStyle = new StyleSheet();
            titleStyle.ID = diagram.StyleSheets.Count + 1;
            titleStyle.Name = "TitleStyle";

            // Define character formatting: font name and size (12 pt -> inches)
            Aspose.Diagram.Char titleChar = new Aspose.Diagram.Char();
            titleChar.IX = 0; // first character run
            titleChar.FontName.Value = "Arial";
            titleChar.Size.Value = 12.0 / 72.0; // convert points to inches
            titleChar.Color.Value = "#000000"; // black text

            // Add the character formatting to the stylesheet
            titleStyle.Chars.Add(titleChar);

            // Add the stylesheet to the diagram's collection
            diagram.StyleSheets.Add(titleStyle);

            // Apply the stylesheet to all shapes whose universal name contains "Title"
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        shape.TextStyle = titleStyle;
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
