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
            // Determine the next available page ID
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Create a background page
            Page backgroundPage = new Page();
            backgroundPage.ID = maxPageId + 1;
            backgroundPage.Name = "BackgroundPage";
            backgroundPage.Background = BOOL.True; // Mark as background

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Apply global protection to prevent editing or deletion of background pages
            diagram.DocumentSettings.ProtectBkgnds = BOOL.True;
            // Optionally protect other aspects of the document as well
            diagram.DocumentSettings.ProtectMasters = BOOL.True;
            diagram.DocumentSettings.ProtectShapes = BOOL.True;
            diagram.DocumentSettings.ProtectStyles = BOOL.True;

            // Save the protected diagram
            diagram.Save("ProtectedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
