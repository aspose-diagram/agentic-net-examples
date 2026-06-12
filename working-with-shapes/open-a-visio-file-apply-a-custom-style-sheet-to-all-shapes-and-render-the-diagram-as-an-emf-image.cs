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
            // Output EMF image path
            string outputPath = "output.emf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a custom style sheet
            StyleSheet customStyle = new StyleSheet();
            customStyle.ID = diagram.StyleSheets.Count + 1;
            // Optional: set a name for the style sheet
            customStyle.Name = "CustomStyle";

            // Define character formatting (e.g., red text, 12pt size)
            Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
            charFormat.IX = 0; // character index
            charFormat.Color.Value = "#FF0000"; // red color
            charFormat.Size.Value = 0.1667; // 12 points = 12/72 inches
            charFormat.Style.Value = StyleValue.Bold; // bold text
            customStyle.Chars.Add(charFormat);

            // Define line formatting (e.g., blue solid line)
            customStyle.Line.LineColor.Value = "#0000FF"; // blue line color
            customStyle.Line.LinePattern.Value = LinePatternValue.Solid; // solid line
            customStyle.Line.LineWeight.Value = 0.02; // line weight in inches

            // Define fill formatting (e.g., green solid fill)
            customStyle.Fill.FillForegnd.Value = "#00FF00"; // green fill color
            customStyle.Fill.FillPattern.Value = 1; // solid fill pattern

            // Add the style sheet to the diagram
            diagram.StyleSheets.Add(customStyle);

            // Apply the custom style sheet to every shape in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shape.TextStyle = customStyle;
                    shape.FillStyle = customStyle;
                    shape.LineStyle = customStyle;
                }
            }

            // Configure image save options for EMF format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf);

            // Save the diagram as an EMF image
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
