using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source VDX file
            string inputPath = "input.vdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the shape with ID 5 from the first page
            long shapeId = 5;
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Create a dynamic string (example: current timestamp)
            string dynamicText = $"Generated on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            // Replace the shape's existing text with the dynamic string
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt(dynamicText));

            // Save the modified diagram back to VDX format
            string outputPath = "output.vdx";
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
