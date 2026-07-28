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
            // Insert a new blank page
            Page newPage = new Page();
            diagram.Pages.Add(newPage);

            // Designate the new page as a background layer
            newPage.Background = BOOL.True;

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
