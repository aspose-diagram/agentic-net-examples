using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Determine the highest existing page ID
        int maxId = 0;
        foreach (Page p in diagram.Pages)
        {
            if (p.ID > maxId)
                maxId = p.ID;
        }

        // Create a new page with an incremented ID
        Page newPage = new Page(maxId + 1);
        newPage.Name = "Page-2";

        // Set the page size to A4 dimensions (in inches)
        newPage.PageSheet.PageProps.PageWidth.Value = 8.27;
        newPage.PageSheet.PageProps.PageHeight.Value = 11.69;

        // Add the new page to the diagram
        diagram.Pages.Add(newPage);

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
