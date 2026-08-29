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

            // Retrieve the shape with ID 10 from the first page
            long targetShapeId = 10L;
            Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                throw new Exception($"Shape with ID {targetShapeId} not found.");
            }

            // Find the built‑in 'Heading 1' stylesheet
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
                throw new Exception("The 'Heading 1' stylesheet was not found in the diagram.");
            }

            // Apply the stylesheet to the shape (text, fill, and line styles)
            shape.TextStyle = headingStyle;
            shape.FillStyle = headingStyle;
            shape.LineStyle = headingStyle;

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
