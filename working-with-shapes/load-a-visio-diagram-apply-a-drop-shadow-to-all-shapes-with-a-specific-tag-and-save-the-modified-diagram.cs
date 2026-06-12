using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Tag value to identify shapes that should receive a drop shadow
            string targetTag = "MyTag";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply shadow only to shapes whose Data1 property matches the target tag
                    if (shape.Data1 == targetTag)
                    {
                        // Enable a simple drop shadow
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                        // Shadow color (black)
                        shape.Fill.ShdwForegnd.Value = "#000000";
                        // Shadow transparency (30% transparent)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;
                        // Shadow offset (horizontal and vertical)
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
