using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Add a first page (optional, ensures the diagram has at least one page)
            Page firstPage = new Page();
            diagram.Pages.Add(firstPage);

            // Add the second page
            Page secondPage = new Page();
            diagram.Pages.Add(secondPage);

            // Set the second page size to A4 dimensions (in inches)
            secondPage.PageSheet.PageProps.PageWidth.Value = 8.27;   // A4 width
            secondPage.PageSheet.PageProps.PageHeight.Value = 11.69; // A4 height

            // Optionally give the page a name
            secondPage.Name = "A4Page";

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
