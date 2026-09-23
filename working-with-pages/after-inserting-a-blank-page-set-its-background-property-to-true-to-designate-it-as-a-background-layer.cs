using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Determine the maximum existing page ID
            int maxPageId = 0;
            foreach (Page existingPage in diagram.Pages)
            {
                if (existingPage.ID > maxPageId)
                    maxPageId = existingPage.ID;
            }

            // Create a new blank page
            Page backgroundPage = new Page();
            backgroundPage.ID = maxPageId + 1;               // Assign a unique ID
            backgroundPage.Name = "BackgroundPage";          // Optional name
            backgroundPage.Background = BOOL.True;           // Designate as a background layer

            // Add the new page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
