using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            using (Diagram diagram = new Diagram())
            {
                // Determine a new unique page ID
                int maxId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxId)
                        maxId = existingPage.ID;
                }

                // Create a new blank page and set it as a background page
                Page backgroundPage = new Page();
                backgroundPage.ID = maxId + 1;               // assign a unique ID
                backgroundPage.Name = "BackgroundPage";      // optional name
                backgroundPage.Background = BOOL.True;       // mark as background layer

                // Add the new page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
        }
    }