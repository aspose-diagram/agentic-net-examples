using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Instantiate a new, empty Diagram object
        Diagram diagram = new Diagram();

        // Create a new page
        Page newPage = new Page();
        newPage.Name = "Page-1";

        // Add the new page to the diagram's Pages collection
        diagram.Pages.Add(newPage);

        // (Optional) Save the diagram to verify the addition
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
