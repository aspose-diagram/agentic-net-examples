using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the existing Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -------------------------------------------------
                // Create a common StyleSheet to be applied to pages
                // -------------------------------------------------
                StyleSheet commonStyle = new StyleSheet();

                // Assign a unique ID for the new style sheet
                commonStyle.ID = diagram.StyleSheets.Count + 1;

                // ----- Character formatting (text) -----
                Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
                charFormat.IX = 0;                                 // character index
                charFormat.Color.Value = "#000000";                // black text color
                charFormat.Size.Value = 0.12;                      // font size (in inches)
                charFormat.Style.Value = StyleValue.Bold;          // bold style
                commonStyle.Chars.Add(charFormat);

                // ----- Line formatting -----
                commonStyle.Line.LineColor.Value = "#FF0000";       // red line color
                commonStyle.Line.LinePattern.Value = LinePatternValue.Solid; // solid line
                commonStyle.Line.LineWeight.Value = 0.02;          // line weight (in inches)

                // ----- Fill formatting -----
                commonStyle.Fill.FillForegnd.Value = "#00FF00";     // green fill color
                // Use integer value for solid fill pattern (1 = solid) to avoid missing enum
                commonStyle.Fill.FillPattern.Value = 1;            // solid fill

                // Add the style sheet to the diagram's collection
                diagram.StyleSheets.Add(commonStyle);

                // -------------------------------------------------
                // Apply the common style sheet to every page in the diagram
                // -------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // ApplyStyle(lineStyleId, fillStyleId, textStyleId)
                    page.ApplyStyle(commonStyle.ID, commonStyle.ID, commonStyle.ID);
                }

                // Save the modified diagram with a valid overload (second argument is SaveFileFormat)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}