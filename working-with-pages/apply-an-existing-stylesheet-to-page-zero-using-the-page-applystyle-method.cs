using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Access the first page (page zero)
            Aspose.Diagram.Page page = diagram.Pages[0];

            // Assume a stylesheet named "MyStyle" already exists in the document
            // Retrieve its ID; if not found, default to -1 (no change)
            int styleId = -1;
            foreach (Aspose.Diagram.StyleSheet style in diagram.StyleSheets)
            {
                if (style.Name == "MyStyle")
                {
                    styleId = style.ID;
                    break;
                }
            }

            // Apply the same style to text, line, and fill of the page
            // Using the retrieved style ID for all three categories
            page.ApplyStyle(styleId, styleId, styleId);

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
