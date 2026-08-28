using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty Visio diagram
        Diagram diagram = new Diagram();

        // Add a second page to the diagram
        Page newPage = new Page();
        // Assign a unique ID (max existing ID + 1) and a name
        int maxId = 0;
        foreach (Page p in diagram.Pages)
        {
            if (p.ID > maxId) maxId = p.ID;
        }
        newPage.ID = maxId + 1;
        newPage.Name = "Page-2";

        diagram.Pages.Add(newPage);

        // Set the page size to A4 dimensions (width = 8.27 inches, height = 11.69 inches)
        newPage.PageSheet.PageProps.PageWidth.Value = 8.27;
        newPage.PageSheet.PageProps.PageHeight.Value = 11.69;

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
