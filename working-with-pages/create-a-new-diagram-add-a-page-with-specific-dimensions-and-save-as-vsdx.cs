using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram (contains a default page)
        Diagram diagram = new Diagram();

        // Add a new page with specific dimensions (in inches)
        Page page = new Page();
        page.PageSheet.PageProps.PageWidth.Value = 8.5;   // Width
        page.PageSheet.PageProps.PageHeight.Value = 11.0; // Height
        diagram.Pages.Add(page);

        // Save the diagram as VSDX format
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
