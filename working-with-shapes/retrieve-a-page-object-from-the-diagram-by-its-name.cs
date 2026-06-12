using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram file
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Name of the page to retrieve
        string targetPageName = "Page-1";

        // Load the diagram (uses Aspose.Diagram's built‑in loading mechanism)
        Diagram diagram = new Diagram(diagramPath);

        // Retrieve the Page object by its name from the Pages collection
        Page page = diagram.Pages.GetPage(targetPageName);

        // At this point 'page' references the requested page and can be used for further operations.
    }
}
