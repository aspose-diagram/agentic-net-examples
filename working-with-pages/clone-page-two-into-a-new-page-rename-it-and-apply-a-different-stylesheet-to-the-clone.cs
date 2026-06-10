using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Verify that a second page exists (pages are zero‑based)
            if (diagram.Pages.Count < 2)
            {
                throw new Exception("The diagram does not contain a second page to clone.");
            }

            // Get the page to be cloned (page two)
            Page sourcePage = diagram.Pages[1];

            // Determine the highest existing page ID to assign a unique ID to the new page
            int maxId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxId) maxId = p.ID;
            }

            // Create a new blank page and set its ID
            Page clonedPage = new Page();
            clonedPage.ID = maxId + 1;

            // Clone the contents of the source page into the new page
            clonedPage.Copy(sourcePage);

            // Rename the cloned page
            clonedPage.Name = "ClonedPage";
            clonedPage.NameU = "ClonedPage";

            // Add the cloned page to the diagram
            diagram.Pages.Add(clonedPage);

            // Create a new stylesheet (different from any existing one)
            StyleSheet newStyle = new StyleSheet();
            newStyle.ID = diagram.StyleSheets.Count + 1;
            newStyle.Name = "NewStyle";

            // Example: set a simple character color in the stylesheet
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0;
            ch.Color.Value = "#FF0000"; // Red text
            newStyle.Chars.Add(ch);

            // Add the new stylesheet to the diagram
            diagram.StyleSheets.Add(newStyle);

            // Apply the new stylesheet to the cloned page
            clonedPage.ApplyStyle(newStyle.ID, newStyle.ID, newStyle.ID);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Dispose the diagram to release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
