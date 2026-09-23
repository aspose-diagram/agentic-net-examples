using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Set the global default font for any new text shape
        FontConfigs.DefaultFontName = "Calibri";

        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Get the first (default) page
        Page page = diagram.Pages[0];

        // Add a text shape that will serve as an annotation
        Shape textShape = page.AddText(2.0, 2.0, 3.0, 1.0, "Sample annotation");

        // Attach a comment (annotation) to the text shape
        page.AddComment(textShape, "Reviewer note");

        // Save the diagram
        diagram.Save("AnnotatedDiagram.vsdx", SaveFileFormat.Vsdx);
    }
}
