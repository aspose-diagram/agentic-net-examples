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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Add a source shape (Rectangle) whose height will be referenced
            // Parameters: PinX, PinY, Width, Height, MasterName, PageNumber
            long sourceId = diagram.AddShape(2.0, 2.0, 2.0, 1.0, "Rectangle", 0);
            Shape sourceShape = page.Shapes.GetShape((int)sourceId);
            // Give the source shape a universal name for easy reference
            sourceShape.NameU = "SourceShape";

            // Add a target shape (Ellipse) that will contain the proportional field
            long targetId = diagram.AddShape(5.0, 2.0, 2.0, 1.0, "Ellipse", 0);
            Shape targetShape = page.Shapes.GetShape((int)targetId);
            targetShape.NameU = "TargetShape";

            // Create a new field on the target shape
            Field proportionalField = new Field();

            // Set the formula to reference the Height cell of the source shape
            // In Visio formulas, referencing another shape's cell uses the shape name, e.g., "Height"
            // Here we rely on the universal name set above; the formula will resolve at runtime
            proportionalField.Value.Ufev.F = "Height";

            // Use an undefined unit (the value will be taken directly from the referenced cell)
            proportionalField.Value.Ufev.Unit = MeasureConst.Undefined;

            // Add the field to the target shape's Fields collection
            targetShape.Fields.Add(proportionalField);

            // Save the diagram to a VSDX file
            diagram.Save("ProportionalScaling.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
