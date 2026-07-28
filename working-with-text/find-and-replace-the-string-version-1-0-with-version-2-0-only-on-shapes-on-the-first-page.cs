using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0)
            Page firstPage = diagram.Pages[0];

            // Loop through each shape on the first page
            foreach (Shape shape in firstPage.Shapes)
            {
                // Replace the exact text "Version 1.0" with "Version 2.0"
                shape.ReplaceText("Version 1.0", "Version 2.0");
                // Refresh shape data after text replacement
                shape.RefreshData();
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
