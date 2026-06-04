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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram(@"C:\Input\sample.vsdx");

            // Create HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Limit the generated image size by setting a maximum page size (width x height in pixels)
            // Adjust the values as needed for your scenario
            htmlOptions.PageSize = new PageSize(800, 600);

            // Save the diagram as HTML with the configured options
            diagram.Save(@"C:\Output\sample.html", htmlOptions);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
