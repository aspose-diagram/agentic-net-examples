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
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape is a group
                    if (shape.Type == TypeValue.Group)
                    {
                        // Group's absolute PinX and PinY
                        double groupPinX = shape.XForm.PinX.Value;
                        double groupPinY = shape.XForm.PinY.Value;

                        // Iterate through sub‑shapes within the group
                        foreach (Aspose.Diagram.Shape subShape in shape.Shapes)
                        {
                            // Sub‑shape's position is relative to the group
                            double relPinX = subShape.XForm.PinX.Value;
                            double relPinY = subShape.XForm.PinY.Value;

                            // Simplified absolute calculation (ignores rotation/scaling)
                            double absPinX = groupPinX + relPinX;
                            double absPinY = groupPinY + relPinY;

                            Console.WriteLine($"Group Shape ID {shape.ID}, Sub‑Shape ID {subShape.ID}: Absolute PinX = {absPinX}, PinY = {absPinY}");
                        }
                    }
                }
            }

            // Save the diagram (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
