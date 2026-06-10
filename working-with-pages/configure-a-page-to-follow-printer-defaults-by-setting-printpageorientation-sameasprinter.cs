using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        // Path to an existing Visio file; if it doesn't exist, create a new blank diagram.
        string inputPath = "input.vsdx";
        Diagram diagram;

        if (System.IO.File.Exists(inputPath))
        {
            diagram = new Diagram(inputPath);
        }
        else
        {
            diagram = new Diagram();
        }

        // Ensure the diagram has at least one page.
        if (diagram.Pages.Count == 0)
        {
            diagram.Pages.Add(new Page());
        }

        // Access the first page.
        Page page = diagram.Pages[0];

        // Set the page to follow printer defaults.
        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.SameAsPrinter;

        // Save the modified diagram.
        diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        // Clean up resources.
        diagram.Dispose();
    }
}
