using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load the Visio diagram that contains the triangle shape
        Diagram diagram = new Diagram("TriangleDiagram.vsdx");

        // Set up SVG save options
        SVGSaveOptions svgOptions = new SVGSaveOptions
        {
            PageIndex = 0,               // render the first page
            SVGFitToViewPort = true      // make the SVG fit the viewport
        };

        // Export the diagram (or the specified page) to an SVG file
        diagram.Save("TriangleDiagram.svg", svgOptions);
    }
}
