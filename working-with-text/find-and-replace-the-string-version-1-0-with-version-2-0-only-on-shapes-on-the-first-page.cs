using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Get the first page (index 0)
            var firstPage = diagram.Pages[0];

            // Iterate through all shapes on the first page
            foreach (Shape shape in firstPage.Shapes)
            {
                // Replace the target text if it exists in the shape
                shape.ReplaceText("Version 1.0", "Version 2.0");

                // Refresh shape data to update geometry after text change
                shape.RefreshData();
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
