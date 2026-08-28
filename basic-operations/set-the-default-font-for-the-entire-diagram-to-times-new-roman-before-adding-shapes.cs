using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Set the global default font for all diagrams to Times New Roman
        FontConfigs.DefaultFontName = "Times New Roman";

        // Create a new diagram instance
        Diagram diagram = new Diagram();

        // Use the first page (a new diagram contains one default page)
        Page page = diagram.Pages[0];

        // Add a text shape; it will use the default font set above
        page.AddText(pinX: 4.25, pinY: 5.5, width: 2.0, height: 0.5, text: "Hello Aspose");

        // Optional: configure save options with the same default font
        DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
        saveOptions.DefaultFont = "Times New Roman";

        // Save the diagram to a VDX file
        diagram.Save("output.vdx", saveOptions);
    }
}
