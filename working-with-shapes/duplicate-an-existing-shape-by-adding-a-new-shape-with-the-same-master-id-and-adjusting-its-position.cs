using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (create/load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page (adjust as needed)
            Page page = diagram.Pages[0];

            // Locate the shape to duplicate (example: shape with NameU "Shape1")
            Shape originalShape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.NameU == "Shape1")
                {
                    originalShape = s;
                    break;
                }
            }

            if (originalShape != null)
            {
                // Create a new shape (create rule)
                Shape newShape = new Shape();

                // Use the same master shape ID as the original
                newShape.MasterShape = originalShape.MasterShape;

                // Optionally copy the name and give it a distinct identifier
                newShape.NameU = originalShape.NameU + "_Copy";

                // Adjust position: offset the original shape by 1 inch (1440 twips)
                double origPinX = originalShape.XForm.PinX.Value;
                double origPinY = originalShape.XForm.PinY.Value;
                newShape.XForm.PinX.Value = origPinX + 1440; // 1 inch to the right
                newShape.XForm.PinY.Value = origPinY + 1440; // 1 inch down

                // Add the new shape to the page (create rule)
                page.Shapes.Add(newShape);
            }

            // Save the modified diagram (save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
