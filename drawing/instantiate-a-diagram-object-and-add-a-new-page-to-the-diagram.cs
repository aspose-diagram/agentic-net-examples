using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Instantiate a new empty Diagram object
        Diagram diagram = new Diagram();

        // Create a new Page instance
        Page newPage = new Page();

        // Optionally set a name for the page
        newPage.Name = "MyPage";

        // Add the newly created page to the diagram's Pages collection
        diagram.Pages.Add(newPage);
    }
}
