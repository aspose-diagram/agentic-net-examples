using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram(@"input.vsdx");

            // Retrieve a shape (for example, the first shape on the first page)
            Shape shape = diagram.Pages[0].Shapes[1];

            // Initialize HTML save options
            HTMLSaveOptions options = new HTMLSaveOptions();

            // Set SaveToolBar to false to exclude shape tooltips from the generated HTML.
            // Set to true if you want the tooltips to be included.
            options.SaveToolBar = false;

            // Export the shape to an HTML file using the specified options
            shape.ToHTML("shape.html", options);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
