using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create a common stylesheet
            StyleSheet commonStyle = new StyleSheet();
            commonStyle.ID = diagram.StyleSheets.Count + 1;

            // Example character formatting: blue text
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0;
            ch.Color.Value = "#0000FF";
            commonStyle.Chars.Add(ch);

            // Example line formatting: red dashed line
            commonStyle.Line.LineColor.Value = "#FF0000";
            commonStyle.Line.LinePattern.Value = LinePatternValue.Dash;

            // Example fill formatting: green fill
            commonStyle.Fill.FillForegnd.Value = "#00FF00";

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(commonStyle);

            // Apply the stylesheet to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                page.ApplyStyle(commonStyle.ID, commonStyle.ID, commonStyle.ID);
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
