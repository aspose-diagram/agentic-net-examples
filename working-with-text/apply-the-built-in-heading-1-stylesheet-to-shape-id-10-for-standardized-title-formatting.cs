using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape with ID 10
            Shape shape = page.Shapes.GetShape(10);
            if (shape == null)
            {
                throw new Exception("Shape with ID 10 not found.");
            }

            // Locate the built‑in stylesheet named "Heading 1"
            StyleSheet headingStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Heading 1")
                {
                    headingStyle = ss;
                    break;
                }
            }
            if (headingStyle == null)
            {
                throw new Exception("Stylesheet 'Heading 1' not found.");
            }

            // Apply the stylesheet to the shape for title formatting
            shape.TextStyle = headingStyle;
            shape.FillStyle = headingStyle;
            shape.LineStyle = headingStyle;

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
