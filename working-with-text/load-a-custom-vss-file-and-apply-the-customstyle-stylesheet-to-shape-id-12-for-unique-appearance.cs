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

            // File paths (adjust as needed)
            string diagramPath = "input.vsdx";
            string stencilPath = "custom.vss";
            string outputPath = "output.vsdx";

            // Load the main diagram
            Diagram diagram = new Diagram(diagramPath);

            // Load the custom stencil (.vss) file – required by the task
            Diagram stencil = new Diagram(stencilPath);

            // Locate the stylesheet named "CustomStyle" in the diagram
            StyleSheet customStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "CustomStyle")
                {
                    customStyle = ss;
                    break;
                }
            }

            // If the stylesheet does not exist, create a simple one
            if (customStyle == null)
            {
                Console.WriteLine("CustomStyle stylesheet not found. Creating a new one.");
                customStyle = new StyleSheet();
                customStyle.Name = "CustomStyle";

                // Example style settings
                customStyle.Line.LineColor.Value = "#FF0000";          // Red line
                customStyle.Fill.FillForegnd.Value = "#00FF00";       // Green fill

                // Character formatting (e.g., blue text)
                var ch = new Aspose.Diagram.Char();
                ch.IX = 0;
                ch.Color.Value = "#0000FF";
                customStyle.Chars.Add(ch);

                diagram.StyleSheets.Add(customStyle);
            }

            // Retrieve shape with ID 12 from the first page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(12L);
            if (shape == null)
            {
                Console.WriteLine("Shape with ID 12 not found.");
            }
            else
            {
                // Apply the stylesheet to the shape
                shape.TextStyle = customStyle;
                shape.LineStyle = customStyle;
                shape.FillStyle = customStyle;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
