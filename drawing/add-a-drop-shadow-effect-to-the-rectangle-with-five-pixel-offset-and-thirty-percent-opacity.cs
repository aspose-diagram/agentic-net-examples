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

            // Locate the first rectangle shape in the document
            Shape rectangle = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        rectangle = shape;
                        break;
                    }
                }
                if (rectangle != null) break;
            }

            if (rectangle == null)
            {
                throw new Exception("Rectangle shape not found in the diagram.");
            }

            // Enable a simple drop shadow
            rectangle.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

            // Set shadow color (black)
            rectangle.Fill.ShdwForegnd.Value = "#000000";

            // Set shadow transparency to 30% (0.3 = 30% transparent)
            rectangle.Fill.ShdwForegndTrans.Value = 0.3;

            // Set shadow offset to 5 pixels.
            // Aspose.Diagram uses inches for measurements; assuming 96 DPI:
            double offsetInches = 5.0 / 96.0;
            rectangle.Fill.ShapeShdwOffsetX.Value = offsetInches;
            rectangle.Fill.ShapeShdwOffsetY.Value = offsetInches;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
