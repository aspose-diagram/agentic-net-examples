using System;
using System.IO;
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

            // Access the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape (pinX, pinY, width, height)
            long shapeId = page.DrawRectangle(2.0, 2.0, 2.0, 1.0);

            // Retrieve the newly added shape and set its text
            Shape shape = page.Shapes.GetShape(shapeId);
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Added shape"));

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Verify the file size reflects the added content
            long fileSize = new FileInfo(outputPath).Length;
            Console.WriteLine($"Diagram saved to '{outputPath}' with size {fileSize} bytes.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
