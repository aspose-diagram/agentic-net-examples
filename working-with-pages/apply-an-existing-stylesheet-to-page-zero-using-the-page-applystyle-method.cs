using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram("input.vsdx");

            // Assume the diagram already contains at least one StyleSheet.
            // Retrieve the ID of the first StyleSheet to use for text, line, and fill.
            int styleId = diagram.StyleSheets[0].ID;

            // Apply the retrieved style to the first page (page zero) using ApplyStyle.
            // The same style ID is used for text, line, and fill formatting.
            diagram.Pages[0].ApplyStyle(styleId, styleId, styleId);

            // Save the modified diagram (lifecycle rule: save)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
