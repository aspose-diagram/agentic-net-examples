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

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (index 0) and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Configure HTML save options to preserve visual styling
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Save the shape as a single HTML file (includes embedded resources)
                SaveAsSingleFile = true,
                // Set resolution (dots per inch) for rendered images within the HTML
                Resolution = 96
            };

            // Export the shape to an HTML file using the built‑in ToHTML method
            shape.ToHTML("shape.html", htmlOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
