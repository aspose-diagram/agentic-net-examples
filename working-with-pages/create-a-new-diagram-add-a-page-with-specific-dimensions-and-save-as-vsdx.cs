using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram (contains a default page)
        Diagram diagram = new Diagram();

        // Add a new page to the diagram
        Page page = new Page();
        diagram.Pages.Add(page);

        // Set the page dimensions (width = 11 inches, height = 8.5 inches)
        page.PageSheet.PageProps.PageWidth.Value = 11.0;
        page.PageSheet.PageProps.PageHeight.Value = 8.5;

        // Save the diagram as VSDX
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
