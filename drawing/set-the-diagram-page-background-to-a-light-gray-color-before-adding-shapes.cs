using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram
        Diagram diagram = new Diagram();

        // Ensure there is at least one page
        if (diagram.Pages.Count == 0)
        {
            // Add a default page if none exist
            diagram.Pages.Add(new Page());
        }

        // Get the first page (the active page)
        Page page = diagram.Pages[0];

        // Apply a fill style to set the page background to light gray.
        // The fillStyleId should correspond to a style in the document that defines a light gray fill.
        // Here we assume style ID 2 is a light gray fill style; adjust as needed.
        int lightGrayFillStyleId = 2;
        page.ApplyStyle(textStyle: -1, lineStyle: -1, fillStyle: lightGrayFillStyleId);

        // TODO: Add shapes to the page here

        // Save the diagram to a file
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
