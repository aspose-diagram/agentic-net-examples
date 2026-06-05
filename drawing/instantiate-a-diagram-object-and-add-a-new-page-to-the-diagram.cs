using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Instantiate a new, empty diagram
        Diagram diagram = new Diagram();

        // Create a new page
        Page newPage = new Page();

        // Optionally set a name for the page
        newPage.Name = "MyPage";

        // Add the page to the diagram's Pages collection
        diagram.Pages.Add(newPage);

        // Save the diagram (using the provided Save method)
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
    }
}
