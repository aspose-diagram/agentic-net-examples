using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Set the global default font that will be used for any new text shape or annotation.
        FontConfigs.DefaultFontName = "Calibri";

        // Create a new empty diagram.
        Diagram diagram = new Diagram();

        // Add a blank page to the diagram (the default constructor creates no pages).
        diagram.Pages.Add(new Page());

        // Retrieve the first (and only) page.
        Page page = diagram.Pages[0];

        // Add an annotation (comment) at the specified coordinates.
        // The annotation will use the default font defined above.
        page.AddComment(2.0, 2.0, "This is an annotation with the default font.");

        // Optionally, add a visible text shape. Since we did not specify a font name,
        // it will also inherit the default font set via FontConfigs.
        page.AddText(4.0, 4.0, 2.0, 1.0, "Sample Text");

        // Save the diagram to a VSDX file.
        diagram.Save("AnnotatedDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
