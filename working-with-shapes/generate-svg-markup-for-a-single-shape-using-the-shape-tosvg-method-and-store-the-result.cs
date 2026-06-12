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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve a shape (e.g., the first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[0];

            // Configure SVG save options (optional settings)
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportElementAsRectTag = true; // example option

            // Define the output SVG file path
            string svgFilePath = "shape.svg";

            // Save the shape to an SVG file
            shape.ToSvg(svgFilePath, svgOptions);

            // Read the generated SVG markup into a string for further use
            string svgMarkup = File.ReadAllText(svgFilePath);

            // Example usage: output the SVG markup to console
            Console.WriteLine(svgMarkup);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
