using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Create a new empty Visio diagram
        using (Diagram diagram = new Diagram())
        {
            // Create a new blank page
            Page newPage = new Page();

            // Add the page to the diagram's page collection
            diagram.Pages.Add(newPage);

            // Optionally set a name for the new page
            newPage.Name = "NewPage";
            newPage.NameU = "NewPage";
        }
    }
}
