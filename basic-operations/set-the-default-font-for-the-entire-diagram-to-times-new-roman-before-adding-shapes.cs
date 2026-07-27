using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram (lifecycle create rule)
        Diagram diagram = new Diagram();

        // Set the default font for the entire diagram before adding any shapes
        FontConfigs.DefaultFontName = "Times New Roman";

        // Optionally add a page to work with (if the diagram has no pages)
        Page page = new Page();
        diagram.Pages.Add(page);

        // Example: add a text shape using the default font
        // Parameters: pinX, pinY, width, height, text, fontName, fontColor, size
        page.AddText(4.25, 5.5, 2.0, 0.5, "Sample Text", "Times New Roman", "0,0,0", 0.2);

        // Prepare save options and also set DefaultFont (ensures correct rendering on save)
        DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
        saveOptions.DefaultFont = "Times New Roman";

        // Save the diagram (lifecycle save rule)
        diagram.Save("output.vdx", saveOptions);
    }
}
