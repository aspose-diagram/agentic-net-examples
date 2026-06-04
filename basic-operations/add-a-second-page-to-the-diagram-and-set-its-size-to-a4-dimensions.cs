using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Determine the next page ID (max existing ID + 1)
        int maxId = 0;
        foreach (Page p in diagram.Pages)
        {
            if (p.ID > maxId)
                maxId = p.ID;
        }

        // Create a new page and assign a unique ID
        Page newPage = new Page();
        newPage.ID = maxId + 1;
        newPage.Name = "Page-2";

        // Set A4 size (width = 8.27 inches, height = 11.69 inches)
        newPage.PageSheet.PageProps.PageWidth.Value = 8.27;
        newPage.PageSheet.PageProps.PageHeight.Value = 11.69;

        // Add the new page to the diagram
        diagram.Pages.Add(newPage);

        // Save the diagram to a VSDX file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
