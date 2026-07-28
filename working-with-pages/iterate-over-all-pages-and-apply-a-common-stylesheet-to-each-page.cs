using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // IDs of the stylesheet elements to apply (replace with actual IDs from your stylesheet)
            int textStyleId = 1; // Text style ID
            int lineStyleId = 2; // Line style ID
            int fillStyleId = 3; // Fill style ID

            // Iterate through all pages in the diagram and apply the common stylesheet
            foreach (Page page in diagram.Pages)
            {
                page.ApplyStyle(textStyleId, lineStyleId, fillStyleId);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
