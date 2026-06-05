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

            // Load the Visio diagram from a file.
            // The Diagram constructor that takes a file path loads the document.
            Diagram diagram = new Diagram("input.vsdx");

            // Create SVG save options.
            SVGSaveOptions svgOptions = new SVGSaveOptions();

            // Set a default font to ensure Unicode characters are rendered correctly
            // when the required font is not installed locally.
            svgOptions.DefaultFont = "Arial Unicode MS";

            // Save the diagram as an SVG file, embedding the font resources as specified.
            diagram.Save("output.svg", svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
