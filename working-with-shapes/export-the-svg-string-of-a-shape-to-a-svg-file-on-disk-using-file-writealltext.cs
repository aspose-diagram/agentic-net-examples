using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ExportShapeSvg
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("inputDiagram.vsdx");

            // Get the first shape on the first page (adjust indices as needed)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Temporary file to let Shape.ToSvg save the SVG content
            string tempSvgPath = Path.Combine(Path.GetTempPath(), "tempShape.svg");

            // Save the shape as SVG using the provided ToSvg method and default options
            shape.ToSvg(tempSvgPath, new SVGSaveOptions());

            // Read the generated SVG content into a string
            string svgContent = File.ReadAllText(tempSvgPath);

            // Define the final SVG file path
            string finalSvgPath = "exportedShape.svg";

            // Write the SVG string to the final file using File.WriteAllText
            File.WriteAllText(finalSvgPath, svgContent);

            // Optional: clean up the temporary file
            if (File.Exists(tempSvgPath))
            {
                File.Delete(tempSvgPath);
            }

            Console.WriteLine($"Shape exported to SVG file: {finalSvgPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
