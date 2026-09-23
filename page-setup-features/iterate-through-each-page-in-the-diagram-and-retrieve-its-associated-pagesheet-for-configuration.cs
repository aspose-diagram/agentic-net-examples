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

            // Iterate through each page in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Retrieve the PageSheet associated with the current page
                Aspose.Diagram.PageSheet pageSheet = page.PageSheet;

                // Example configuration: set the background color of the page sheet (optional)
                // pageSheet.FillColor = System.Drawing.Color.LightGray;

                // Additional configuration can be performed here using pageSheet properties
            }

            // Save the diagram after any modifications (if needed)
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
