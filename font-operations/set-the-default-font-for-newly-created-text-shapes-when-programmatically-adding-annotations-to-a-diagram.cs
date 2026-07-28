using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (lifecycle create rule)
        Diagram diagram = new Diagram();

        // Set the default font for the diagram – new text shapes will inherit this font
        FontConfigs.DefaultFontName = "Arial";

        // Add a page to the diagram (if the diagram has no pages)
        Page page = new Page();
        diagram.Pages.Add(page);

        // Add a text shape without specifying a font name; it uses the default font set above
        page.AddText(pinX: 5.0, pinY: 5.0, width: 2.0, height: 0.5, text: "Sample annotation");

        // Configure save options and also set DefaultFont for formats that require it (e.g., PDF)
        PdfSaveOptions saveOptions = new PdfSaveOptions
        {
            DefaultFont = "Arial"
        };

        // Save the diagram (lifecycle save rule)
        diagram.Save("output.pdf", saveOptions);
    }
}
