using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder containing Visio files.
        string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Get all .vsdx files in the folder (non‑recursive).
        string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

        foreach (string filePath in diagramFiles)
        {
            // Load the diagram.
            Diagram diagram = new Diagram(filePath);

            // Create a custom stylesheet.
            StyleSheet customStyle = new StyleSheet();
            customStyle.ID = diagram.StyleSheets.Count + 1;
            customStyle.Name = "CustomStyle";

            // Character formatting (e.g., black text).
            Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
            charFormat.IX = 0;
            charFormat.Color.Value = "#000000";
            customStyle.Chars.Add(charFormat);

            // Line formatting (red dashed line, thin weight).
            customStyle.Line.LineColor.Value = "#FF0000";
            customStyle.Line.LinePattern.Value = LinePatternValue.Dash;
            customStyle.Line.LineWeight.Value = 0.02;

            // Fill formatting (green solid fill).
            customStyle.Fill.FillForegnd.Value = "#00FF00";
            customStyle.Fill.FillPattern.Value = 1;

            // Add the stylesheet to the diagram.
            diagram.StyleSheets.Add(customStyle);

            // Apply the stylesheet to every shape on every page.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shape.TextStyle = customStyle;
                    shape.LineStyle = customStyle;
                    shape.FillStyle = customStyle;
                }
            }

            // Save the updated diagram with a new name.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(filePath),
                Path.GetFileNameWithoutExtension(filePath) + "_styled.vsdx");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Processed: {filePath} -> {outputPath}");
        }
    }
}
