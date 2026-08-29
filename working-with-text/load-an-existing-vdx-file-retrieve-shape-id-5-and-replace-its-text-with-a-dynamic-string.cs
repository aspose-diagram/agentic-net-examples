using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination VDX files
            string inputPath = "input.vdx";
            string outputPath = "output.vdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the shape with ID 5
            Shape shape = page.Shapes.GetShape(5);

            // Create a dynamic string (example: current timestamp)
            string dynamicText = $"Generated on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            // Replace the shape's existing text
            shape.Text.Value.Clear();                     // Remove old text runs
            shape.Text.Value.Add(new Txt(dynamicText));   // Add new text run

            // Save the modified diagram back to VDX format
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
