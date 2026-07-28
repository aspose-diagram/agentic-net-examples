using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram file
            var diagram = new Diagram("input.vsdx");

            // Retrieve a shape (e.g., the first shape on the first page)
            var shape = diagram.Pages[0].Shapes[0];

            // Create a temporary file path for the intermediate SVG export
            string tempSvgPath = Path.GetTempFileName();

            // Export the shape to SVG using Aspose.Diagram's ToSvg method
            var svgOptions = new SVGSaveOptions();
            shape.ToSvg(tempSvgPath, svgOptions);

            // Read the generated SVG content into a string
            string svgContent = File.ReadAllText(tempSvgPath);

            // Write the SVG string to the final .svg file using File.WriteAllText
            string outputSvgPath = "shape.svg";
            File.WriteAllText(outputSvgPath, svgContent);

            // Delete the temporary file
            File.Delete(tempSvgPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
