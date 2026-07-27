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

            // Load the source Visio diagram
            var diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Create HTML save options
            var htmlOptions = new HTMLSaveOptions();

            // Limit the generated image size by setting the page size (width, height in points)
            // Example: maximum width = 800 points, maximum height = 600 points
            htmlOptions.PageSize = new PageSize(800, 600);

            // Optionally adjust resolution (dots per inch) if needed
            // htmlOptions.Resolution = 96;

            // Save the diagram as HTML with the configured options
            diagram.Save(@"C:\Output\sample.html", htmlOptions);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
