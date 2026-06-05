using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Assume the rectangle is on the first page
            Page page = diagram.Pages[0];

            // Find the first shape whose master name is "Rectangle"
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Rectangle")
                {
                    // Enable simple drop shadow
                    shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

                    // Set shadow color to black
                    shape.Fill.ShdwForegnd.Value = "#000000";

                    // Set shadow opacity to 30% (0.3 = 30% transparent)
                    shape.Fill.ShdwForegndTrans.Value = 0.3;

                    // Set shadow offsets (5 units; Visio uses inches, adjust as needed)
                    shape.Fill.ShapeShdwOffsetX.Value = 5;
                    shape.Fill.ShapeShdwOffsetY.Value = 5;

                    // Shadow applied; exit loop
                    break;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
