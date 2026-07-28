using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Assume we want to modify the first page; adjust as needed
                Page page = diagram.Pages[0];

                // Set page orientation to Landscape
                page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                // Adjust page dimensions for landscape orientation.
                // Example: set width to 11 inches and height to 8.5 inches (standard Letter landscape)
                page.PageSheet.PageProps.PageWidth.Value = 11.0;
                page.PageSheet.PageProps.PageHeight.Value = 8.5;

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
