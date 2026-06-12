using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Retrieve an existing shape to clone (here we take the first shape on the page)
                // Adjust the ID as needed for your specific diagram
                Shape originalShape = page.Shapes.GetShape(1);

                // Determine the master name of the original shape; fallback to a basic master if null
                string masterName = originalShape.Master != null ? originalShape.Master.Name : "Rectangle";

                // Add a new shape on the same page, offsetting its position slightly
                double offsetX = 2.0; // inches to the right
                double newPinX = originalShape.XForm.PinX.Value + offsetX;
                double newPinY = originalShape.XForm.PinY.Value;

                long clonedShapeId = page.AddShape(newPinX, newPinY, masterName);
                Shape clonedShape = page.Shapes.GetShape(clonedShapeId);

                // Inherit fill settings from the original shape
                clonedShape.Fill.FillForegnd.Value = originalShape.Fill.FillForegnd.Value;
                clonedShape.Fill.FillBkgnd.Value = originalShape.Fill.FillBkgnd.Value;
                clonedShape.Fill.FillPattern.Value = originalShape.Fill.FillPattern.Value;
                clonedShape.Fill.FillForegndTrans.Value = originalShape.Fill.FillForegndTrans.Value;
                clonedShape.Fill.FillBkgndTrans.Value = originalShape.Fill.FillBkgndTrans.Value;

                // Modify the line inheritance flag by setting a distinct line color
                // This breaks line inheritance and applies the new color to the cloned shape
                clonedShape.Line.LineColor.Value = "#FF0000"; // Red line

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }