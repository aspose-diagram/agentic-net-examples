using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load a Visio diagram (replace with your actual file path)
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page and log its dimensions
            foreach (Page page in diagram.Pages)
            {
                double width = page.PageSheet.PageProps.PageWidth.Value;   // width in inches
                double height = page.PageSheet.PageProps.PageHeight.Value; // height in inches

                Console.WriteLine($"Page \"{page.Name}\" - Width: {width} inches, Height: {height} inches");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
