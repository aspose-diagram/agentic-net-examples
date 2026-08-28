using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram. This automatically contains one default page.
        Diagram diagram = new Diagram();

        // Determine the next page ID (max existing ID + 1) to avoid ID conflicts.
        int maxPageId = 0;
        foreach (Page existingPage in diagram.Pages)
        {
            if (existingPage.ID > maxPageId)
                maxPageId = existingPage.ID;
        }

        // Create a new page with a unique ID.
        Page newPage = new Page(maxPageId + 1);
        newPage.Name = "CustomPage";
        // Set specific dimensions (width = 11 inches, height = 8.5 inches for example).
        newPage.PageSheet.PageProps.PageWidth.Value = 11.0;
        newPage.PageSheet.PageProps.PageHeight.Value = 8.5;

        // Add the new page to the diagram.
        diagram.Pages.Add(newPage);

        // Optionally, modify the default page dimensions as well.
        Page defaultPage = diagram.Pages[0];
        defaultPage.PageSheet.PageProps.PageWidth.Value = 11.0;
        defaultPage.PageSheet.PageProps.PageHeight.Value = 8.5;

        // Save the diagram as VSDX using the correct SaveFileFormat enum.
        string outputPath = "output.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
