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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create a simple stylesheet
            StyleSheet style = new StyleSheet();
            style.ID = diagram.StyleSheets.Count + 1;

            // Character formatting (red text)
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0;
            ch.Color.Value = "#FF0000";
            style.Chars.Add(ch);

            // Line formatting (green line)
            style.Line.LineColor.Value = "#00FF00";

            // Fill formatting (blue fill)
            style.Fill.FillForegnd.Value = "#0000FF";

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(style);

            // Index of the page we want to style (example: a non‑existent page)
            int pageIndex = 5;

            try
            {
                // Attempt to retrieve the page; will throw if index is out of range
                Page page = diagram.Pages[pageIndex];

                // Apply the stylesheet to the page (master, line, and fill styles)
                page.ApplyStyle(style.ID, style.ID, style.ID);

                Console.WriteLine($"Successfully applied stylesheet to page index {pageIndex}.");
            }
            catch (Exception ex)
            {
                // Handle errors such as invalid page index
                Console.WriteLine($"Error: Unable to apply stylesheet to page index {pageIndex}. {ex.Message}");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
