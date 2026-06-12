using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Enable line inheritance only for connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Copy inherited line formatting to the shape's explicit line cells
                        // This effectively makes the connector use the inherited line style
                        shape.Line.LineColor.Value = shape.InheritLine.LineColor.Value;
                        shape.Line.LineWeight.Value = shape.InheritLine.LineWeight.Value;
                        shape.Line.LinePattern.Value = shape.InheritLine.LinePattern.Value;
                        shape.Line.BeginArrow.Value = shape.InheritLine.BeginArrow.Value;
                        shape.Line.EndArrow.Value = shape.InheritLine.EndArrow.Value;
                        shape.Line.BeginArrowSize.Value = shape.InheritLine.BeginArrowSize.Value;
                        shape.Line.EndArrowSize.Value = shape.InheritLine.EndArrowSize.Value;
                        shape.Line.LineCap.Value = shape.InheritLine.LineCap.Value;
                        shape.Line.Rounding.Value = shape.InheritLine.Rounding.Value;
                    }
                }
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
