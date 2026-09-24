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
            string inputPath = "input.vsdx";   // TODO: replace with actual file path
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Identify the shape to clone (replace with the actual shape ID)
            long originalShapeId = 1; // example ID
            Shape originalShape = page.Shapes.GetShape(originalShapeId);
            if (originalShape == null)
            {
                throw new Exception($"Shape with ID {originalShapeId} not found.");
            }

            // Add a new shape using the same master as the original shape.
            // Position it 2 inches to the right of the original shape.
            double newPinX = originalShape.XForm.PinX.Value + 2.0;
            double newPinY = originalShape.XForm.PinY.Value;
            string masterName = originalShape.Master?.Name ?? throw new Exception("Original shape has no master.");
            long newShapeId = page.AddShape(newPinX, newPinY, masterName, false);
            Shape clonedShape = page.Shapes.GetShape(newShapeId);

            // Copy basic visual properties (size, text, fill, line) to preserve appearance.
            clonedShape.XForm.Width.Value = originalShape.XForm.Width.Value;
            clonedShape.XForm.Height.Value = originalShape.XForm.Height.Value;
            clonedShape.Text.Value.Clear();
            foreach (var txt in originalShape.Text.Value)
            {
                if (txt is Txt t)
                    clonedShape.Text.Value.Add(new Txt(t.Text));
            }
            clonedShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
            clonedShape.Fill.FillPattern.Value = originalShape.Fill.FillPattern.Value;
            clonedShape.Line.LineColor.Value = originalShape.Line.LineColor.Value;
            clonedShape.Line.LineWeight.Value = originalShape.Line.LineWeight.Value;
            clonedShape.Line.LinePattern.Value = originalShape.Line.LinePattern.Value;

            // Preserve gluing: rewire all connectors that were glued to the original shape
            // to point to the cloned shape instead, keeping the same connection cells.
            foreach (Connect conn in page.Connects)
            {
                if (conn.FromSheet == originalShapeId)
                    conn.FromSheet = newShapeId;
                if (conn.ToSheet == originalShapeId)
                    conn.ToSheet = newShapeId;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx"; // TODO: replace with desired output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
