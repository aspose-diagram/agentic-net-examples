using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Set the default font name that will be applied to text shapes
        FontConfigs.DefaultFontName = "Arial";

        // Get the first page (a new diagram contains at least one page)
        Page page = diagram.Pages[0];

        // Add a text shape without specifying a font name.
        // The shape will inherit the default font set above.
        // Parameters: pinX, pinY, width, height, text
        page.AddText(5.0, 5.0, 2.0, 0.5, "Sample annotation");

        // Create save options for the desired output format (PDF in this example)
        PdfSaveOptions saveOptions = (PdfSaveOptions)SaveOptions.CreateSaveOptions(SaveFileFormat.Pdf);
        // Ensure the same default font is used during export
        saveOptions.DefaultFont = "Arial";

        // Save the diagram with the configured options
        diagram.Save("AnnotatedDiagram.pdf", saveOptions);
    }
}
